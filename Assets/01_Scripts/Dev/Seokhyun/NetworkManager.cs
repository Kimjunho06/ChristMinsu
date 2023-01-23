using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.WebSockets;
using System;
using System.Threading;
using ChristMinsu.Packet;
using Google.Protobuf;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance = null;
    public static NetworkManager Instance 
    { 
        get {
            if(_instance == null)
                _instance = FindObjectOfType<NetworkManager>();
            return _instance;
        } 
    }
    
    private ClientWebSocket _socket = null;


    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("Multiple NetworkManager Instance is running!");
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        Connection();
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    public void SendName(string value)
    {
        Name name = new Name();
        name.Value = "ㅅㅂ";
        ushort len = (ushort)(name.CalculateSize() + 4);

        ArraySegment<byte> segment = new ArraySegment<byte>(new byte[len]);
        Array.Copy(BitConverter.GetBytes(len), 0, segment.Array, segment.Offset, sizeof(ushort));
        Array.Copy(BitConverter.GetBytes((ushort)MSGID.Name), 0, segment.Array, segment.Offset + 2, sizeof(ushort));
        Array.Copy(name.ToByteArray(), 0, segment.Array, segment.Offset + 4, len - 4);

        SendData(segment);
    }

    public async void Connection()
    {
        Debug.Log("Start to open Socket");
        if(_socket != null && _socket.State == WebSocketState.Open)
        {
            Debug.Log("Socket is already Open!");
            return;
        }

        _socket = new ClientWebSocket();
        Uri uri = new Uri("ws://localhost:50000");

        await _socket.ConnectAsync(uri, CancellationToken.None);
        Debug.Log("Socket is now Open!");
    }

    public async void SendData(ArraySegment<byte> segment)
    {
        await _socket.SendAsync(segment, WebSocketMessageType.Binary, true, CancellationToken.None);
    }

    public void Disconnect()
    {
        if(_socket != null)
        {
            _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Quit", CancellationToken.None);
            Debug.Log("Socket is Disconnected!");
        }
    }
}
