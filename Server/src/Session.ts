import WebSocket from "ws";
import crypto from "crypto";
import { SessionState } from "./SessionState";

export default class Session
{
    socket: WebSocket;
    id: string
    name: string;

    state: SessionState = SessionState.LOGOUT;

    constructor(socket: WebSocket) {
        this.socket = socket;
        this.id = crypto.randomUUID();
        this.name = "";
        this.state = SessionState.LOGOUT;
    }

    login(name: string): void {
        this.state = SessionState.LOGIN;
        this.name = name;
    }
}