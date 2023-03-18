using System.Collections;
using System.Collections.Generic;
using ChristMinsu.Packet;
using Google.Protobuf;
using UnityEngine;

public class LoginResHandler : IPacketHandler
{
    public void Process(IMessage packet)
    {
        GameObject.FindObjectOfType<LoginPage>().ShowRes(packet as LoginRes, ResType.Login);
    }
}
