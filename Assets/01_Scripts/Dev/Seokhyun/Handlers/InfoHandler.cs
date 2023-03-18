using ChristMinsu.Packet;
using Google.Protobuf;
using TMPro;
using UnityEngine;

public class InfoHandler : IPacketHandler
{
    public void Process(IMessage packet)
    {
        // Transform canvas = ChristMinsu.DevSeok.GameManager.Instance.SocketInfoCanvas;
        // TextMeshProUGUI uuid = canvas.Find("UUID").GetComponent<TextMeshProUGUI>();
        // uuid.text = $"UUID : {(packet as SessionInfo).Uuid}";
    }
}