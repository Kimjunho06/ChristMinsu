import http, { createServer } from "http";
import Express, { Application } from "express";
import WebSocket, { AddressInfo, RawData, Server } from "ws";
import { christMinsu } from "./packet/packet";
import crypto from "crypto";
import Session from "./Session";
import SessionManager from "./SessionManager";
import PacketManager from "./PacketManager";

const App: Application = Express();

App.get("/", (req, res) => {
    res.json(SessionManager.Instance.sessionMap);
})

const httpServer = App.listen(50000, () => {
    console.log("Http Server is running on port 50000");
});

const wss: Server = new Server({
    server: httpServer
});

PacketManager.Instance = new PacketManager();
SessionManager.Instance = new SessionManager();

wss.on("listening", () => {
    console.log("WS Server is running on port 50000");
});

wss.on("connection", (soc: WebSocket, req: http.IncomingMessage) => {
    const uuid = crypto.randomUUID();
    let session = new Session(soc, uuid, () => {
        SessionManager.Instance.removeSession(uuid);
    });
    SessionManager.Instance.addSession(session, uuid);
    console.log(
        `New Session login. id: ${uuid}, ip: ${req.connection.remoteAddress}`
    );

    let info = new christMinsu.SessionInfo({name: "", uuid:uuid});
    session.sendData(info.serialize(), christMinsu.MSGID.SessionINFO);

    soc.on("message", (rawData: RawData, isBinary: boolean) => {
        if(isBinary)
            session.receiveMsg(rawData);
    });
});