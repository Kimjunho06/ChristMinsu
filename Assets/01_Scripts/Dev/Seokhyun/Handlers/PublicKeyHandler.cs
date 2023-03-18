using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using UnityEngine;
using ChristMinsu.Packet;

public class PublicKeyHandler : IPacketHandler
{
    public void Process(IMessage packet)
    {
        GameObject.FindObjectOfType<LoginPage>().SetPublicKey(packet as PublicKey);
    }
}
