import { ChatHandler } from "./handlers/ChatHandler";
import { NameHandler } from "./handlers/NameHandler";
import { PacketHandler } from "./packet/PacketHandler";
import { christMinsu } from "./packet/packet";
import { RegisterHandler } from "./handlers/RegisterHandler";
import { LoginHandler } from "./handlers/LoginHandler";

export default class PacketManager
{
    static Instance: PacketManager;
    handlerMap: { [key: number]: PacketHandler };

    constructor() {
        this.handlerMap = {};
        this.registerPacket();
    }

    registerPacket(): void {
        this.handlerMap[christMinsu.MSGID.NAME] = NameHandler;
        this.handlerMap[christMinsu.MSGID.CHAT] = ChatHandler;
        this.handlerMap[christMinsu.MSGID.RegisterREQ] = RegisterHandler;
        this.handlerMap[christMinsu.MSGID.LoginREQ] = LoginHandler;
    }
}