using System.Collections;
using System.Collections.Generic;
using ChristMinsu.Packet;
using Google.Protobuf;
using UnityEngine;

public class ChatHandler : IPacketHandler
{
    public void Process(IMessage packet)
    {
        Chat chat = packet as Chat;
        ChatManager.Instance.AddChat(chat.Name, chat.Value);
    }
}
