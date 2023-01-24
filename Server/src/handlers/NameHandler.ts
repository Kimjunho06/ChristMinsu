import Session from "../Session";
import SessionManager from "../SessionManager";
import { SessionState } from "../SessionState";
import { PacketHandler } from "../packet/PacketHandler";
import { christMinsu } from "../packet/packet";

export const NameHandler: PacketHandler = {
    handleMsg(session: Session, buffer: Buffer) {
        let name = christMinsu.Name.deserialize(buffer);
        session.name = name.value;
        session.state = SessionState.LOGIN;

        let scene = new christMinsu.ChangeScene({name:"InGame"});
        session.sendData(scene.serialize(), christMinsu.MSGID.ChangeSCENE);
        
    }
}