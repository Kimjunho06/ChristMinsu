import { christMinsu } from "../packet/packet";
import { PacketHandler } from "../packet/PacketHandler";
import Session from "../Session";
import SessionManager from "../SessionManager";
import { SessionState } from "../SessionState";
import { Pool } from "../DB";

export const LoginHandler: PacketHandler = {
    async handleMsg(session: Session, buffer: Buffer) {
        let req = christMinsu.LoginReq.deserialize(buffer);
        let sql = `SELECT (name) FROM user WHERE name = ? AND password = ?;`;

        let result = await Pool.query(sql, [req.name, req.pw]);
        console.log((result[0] as []).length);
        const res = new christMinsu.LoginRes({success: (result[0] as []).length != 0});
        session.sendData(res.serialize(), christMinsu.MSGID.LoginRES);
    }
}