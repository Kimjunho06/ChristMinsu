import http, { createServer } from "http";
import Express, { Application } from "express";
import WebSocket, { RawData, Server } from "ws";
import Session from "./Session";
import { christMinsu } from "./packet/packet";

const App: Application = Express();

const httpServer = App.listen(50000, () => {
    console.log("Http Server is running on port 50000");
});

const wss: Server = new Server({
    server: httpServer
});

wss.on("listening", (soc: WebSocket) => {
    console.log("WS Server is running on port 50000");
});

wss.on("connection", (soc) => {
    let session = new Session(soc);
    console.log(`New Session Login. ${session.id}`);

    soc.on("message", (rawData: RawData, isBinary: boolean) => {
        let length: number = (rawData.slice(0, 2) as Buffer).readInt16LE();
        let code: number = (rawData.slice(2, 4) as Buffer).readInt16LE();
        let payload: Buffer = rawData.slice(4) as Buffer;
        console.log(`Get Packet. length: ${length}, code: ${code}`);

        if(code == 0)
        {
            let name: christMinsu.Name = christMinsu.Name.deserialize(payload);
            console.log(`Session Name: ${name.value}, id: ${session.id}`);
            session.name = name.value;
        }
    });

    soc.on("close", (code: number, reason: Buffer) => {
        console.log(`Session Disconnected. ${session.id}`);
    })
});