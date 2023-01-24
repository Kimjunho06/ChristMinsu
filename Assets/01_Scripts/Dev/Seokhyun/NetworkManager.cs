using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.WebSockets;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChristMinsu.Packet;
using Google.Protobuf;
using TMPro;
using Unity.VisualScripting;

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

    private TextMeshProUGUI _socketState = null;
    private TextMeshProUGUI _socketUUID = null;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("Multiple NetworkManager Instance is running!");
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        Transform canvas = GameObject.Find("Canvas").transform;
        _socketState = canvas.Find("SocketState").GetComponent<TextMeshProUGUI>();
        _socketUUID = canvas.Find("UUID").GetComponent<TextMeshProUGUI>();

        UpdateSocketState();
    }

    private void Start()
    {
        // Connection();
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    public void SendName(string value)
    {
        Name name = new Name();
        name.Value = value;
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
            UpdateSocketState();
            return;
        }

        _socket = new ClientWebSocket();
        Uri uri = new Uri("ws://localhost:50000");

        try
        {
            await _socket.ConnectAsync(uri, CancellationToken.None);
        }
        catch (Exception ex)
        {
            _socket = null;
            Debug.LogError("Failed to connect");
            Debug.LogException(ex);
            UpdateSocketState();
            return;
        }
        Debug.Log("Socket is now Open!");
        
        UpdateSocketState();
    }

    public async void SendData(ArraySegment<byte> segment)
    {
        if(_socket == null || _socket.State != WebSocketState.Open)
        {
            Debug.LogError("Socket is not connected. Can't send Data");
            return;
        }
        await _socket.SendAsync(segment, WebSocketMessageType.Binary, true, CancellationToken.None);
    }

    public void Disconnect()
    {
        if(_socket != null)
        {
            _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Quit", CancellationToken.None);
            Debug.Log("Socket is Disconnected!");
            UpdateSocketState();
        }
    }

    private void UpdateSocketState()
    {
        if(_socket != null && _socket.State == WebSocketState.Open)
        {
            _socketState.text = "접속 상태 - 접속됨";
            _socketUUID.text = "UUID : {_socket.GetUUID()}";
        }
        else
        {
            _socketState.text = "접속 상태 - 접속되지 않음";
            _socketUUID.text = string.Empty;
        }
    }
}
