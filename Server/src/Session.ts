import { RawData, WebSocket } from "ws";
import PacketManager from "./PacketManager";
import { christMinsu } from "./packet/packet";
import { SessionState } from "./SessionState";
import RSAManager from "./RSA";

export default class Session
{
    socket: WebSocket;
    uuid: string;
    name: string = "";
    rsa: RSAManager;

    state: SessionState;

    constructor(socket: WebSocket, uuid: string, rsaManager: RSAManager, OnClose: Function) {
        this.socket = socket;
        this.uuid = uuid;
        this.state = SessionState.LOGOUT;
        this.rsa = rsaManager;

        let { modulus, exponent } = this.rsa;
        this.sendData(new christMinsu.PublicKey({modulus, exponent}).serialize(), christMinsu.MSGID.PublicKEY);
        
        this.socket.on("close", (code: number, reason: Buffer) => {
            console.log(`Session ${uuid} Disconnected. Close code : ${code}`);
            OnClose();
        });
    }
    
    private getInt16LEFromBuffer(buffer:Buffer): number
    {
        return buffer.readInt16LE();
    }

    receiveMsg(data: RawData):void
    {
        let code:number = this.getInt16LEFromBuffer(data.slice(2, 4) as Buffer);
        console.log(`Packet received. code: ${christMinsu.MSGID[code]}`);
        PacketManager.Instance.handlerMap[code].handleMsg(this, data.slice(4) as Buffer);
    }

    sendData(payload: Uint8Array, msgCode:number): void 
    {
        let len: number = payload.length + 4;

        let lenBuffer: Uint8Array = new Uint8Array(2); 
        new DataView(lenBuffer.buffer).setUint16(0, len, true);

        let msgCodeBuffer: Uint8Array = new Uint8Array(2);
        new DataView(msgCodeBuffer.buffer).setUint16(0, msgCode, true);

        let sendBuffer: Uint8Array = new Uint8Array(len);
        sendBuffer.set(lenBuffer, 0);
        sendBuffer.set(msgCodeBuffer, 2);
        sendBuffer.set(payload, 4);

        this.socket.send(sendBuffer);
    }
}