import Session from "../Session";

export interface PacketHandler
{
    handleMsg(session: Session, buffer: Buffer): void;
}