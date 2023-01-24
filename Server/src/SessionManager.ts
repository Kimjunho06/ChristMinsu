import Session from "./Session";
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

    addSession(session: Session, id: string): void {
        this.sessionMap[id] = session;
    }

    removeSession(id: string): void {
        delete this.sessionMap[id];
    }
}