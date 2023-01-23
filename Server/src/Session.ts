import WebSocket from "ws";
import crypto from "crypto";

export default class Session
{
    socket: WebSocket;
    id: string
    name: string;

    constructor(socket: WebSocket) {
        this.socket = socket;
        this.id = crypto.randomUUID();
        this.name = "";
    }
}