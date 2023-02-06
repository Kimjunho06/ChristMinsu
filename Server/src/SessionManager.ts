import Session from "./Session";
import { SessionState } from "./SessionState";
import { christMinsu } from "./packet/packet";

export default class SessionManager
{
    static Instance: SessionManager;
    sessionMap: { [key: string] : Session };

    constructor() {
        this.sessionMap = {};
    }

    getSession(id: string): Session | undefined {
        return this.sessionMap[id];
    }

    broadcast(payload: Uint8Array, msgCode: number): void {
        Object.values(this.sessionMap).forEach(s => {
            if(s.state == SessionState.LOGIN)
            {
                s.sendData(payload, msgCode);
            }
        });
    }

    addSession(session: Session, id: string): void {
        this.sessionMap[id] = session;
    }

    removeSession(id: string): void {
        delete this.sessionMap[id];
    }
}