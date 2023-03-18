import { christMinsu } from "../packet/packet";
import { PacketHandler } from "../packet/PacketHandler";
import Session from "../Session";
import SessionManager from "../SessionManager";
import { SessionState } from "../SessionState";
import { Pool } from "../DB";

export const RegisterHandler: PacketHandler = {
    async handleMsg(session: Session, buffer: Buffer) {
        let req = christMinsu.RegisterReq.deserialize(buffer);

        let sq = `SELECT * FROM user WHERE name = ?`;
        let result = await Pool.query(sq, [req.name]);
        if((result[0] as []).length != 0) {
            let res = new christMinsu.RegisterRes({success: false});
            session.sendData(res.serialize(), christMinsu.MSGID.RegisterRES);
            return;
        }
        
        let sql = `INSERT INTO user (create_time, name, password, public_key, private_key) 
                   VALUES (CURRENT_TIMESTAMP,?,?,?,?)`;
        await Pool.query(sql, [req.name, req.pw, session.rsa.publicKey, session.rsa.privateKey]);

    }
}