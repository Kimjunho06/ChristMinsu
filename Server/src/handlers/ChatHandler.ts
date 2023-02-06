import SessionManager from "../SessionManager";
import { PacketHandler } from "../packet/PacketHandler";
import { christMinsu } from "../packet/packet";

export const ChatHandler: PacketHandler = {
    handleMsg(session, buffer) {
        let chat = christMinsu.Chat.deserialize(buffer);
        if(chat.value.trim() == "") 
        {
            let msgBox = new christMinsu.MsgBox({msg:"빈 채팅입니다."});
            session.sendData(msgBox.serialize(), christMinsu.MSGID.MSGBOX);
            return;
        }
        SessionManager.Instance.broadcast(buffer, christMinsu.MSGID.CHAT);
    }
}