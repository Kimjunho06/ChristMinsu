import Session from "../Session";
import SessionManager from "../SessionManager";
import { SessionState } from "../SessionState";
import { PacketHandler } from "../packet/PacketHandler";
import { christMinsu } from "../packet/packet";

export const NameHandler: PacketHandler = {
    handleMsg(session: Session, buffer: Buffer) {
        let name = christMinsu.Name.deserialize(buffer);
        if(name.value == "")
        {
            let box = new christMinsu.MsgBox({msg: "이름이 빈 값입니다. 이름을 입력하세요."});
            session.sendData(box.serialize(), christMinsu.MSGID.MSGBOX);
            return;
        }
        session.name = name.value;
        session.state = SessionState.LOGIN;
        SessionManager.Instance.broadcast(name.serialize(), christMinsu.MSGID.NAME);

        let scene = new christMinsu.ChangeScene({sceneName:"InGame", sessionName:session.name});
        session.sendData(scene.serialize(), christMinsu.MSGID.ChangeSCENE);
    }
}