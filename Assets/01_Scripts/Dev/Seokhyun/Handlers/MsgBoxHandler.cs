using ChristMinsu.DevSeok;
using ChristMinsu.Packet;
using Google.Protobuf;
using TMPro;
using UnityEngine;

public class MsgBoxHandler : IPacketHandler
{
    public void Process(IMessage packet)
    {
        MsgBox box = packet as MsgBox;
        Debug.Log(box.Msg);
    }
}