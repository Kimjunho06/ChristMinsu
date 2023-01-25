using ChristMinsu.Packet;
using Google.Protobuf;

public class EnterHandler : IPacketHandler
{
    public void Process(IMessage packet)
    {
        Name name = packet as Name;

        // ChatManager.Instance.Enter(name);
    }
}