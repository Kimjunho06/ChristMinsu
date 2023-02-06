using System;
using System.Collections;
using System.Collections.Generic;
using ChristMinsu.Packet;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    private static ChatManager _instance = null;
    public static ChatManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = FindObjectOfType<ChatManager>();
            return _instance;
        }
    }


    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _chatObj, _enterObj;
    
    private string _sessionName = string.Empty;
    public string SessionName { get => _sessionName; set => _sessionName = value; }

    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogError("Multiple ChatManager Instance is running! Destroy this!");
            Destroy(gameObject);
        }
        _instance = this;
    }

    public void AddChat(string name)
    {
        name += " 님이 입장했습니다.";
        EnterElement element = Instantiate(_enterObj, _content).GetComponent<EnterElement>();
        element.SetText(name);
    }
    public void AddChat(string name, string value)
    {
        ChatElement element = Instantiate(_chatObj, _content).GetComponent<ChatElement>();
        element.SetText(name, value);
    }

    public void SendChat(string value)
    {
        Chat chat = new Chat { Name = SessionName, Value = value };
        NetworkManager.Instance.RegisterSend(MSGID.Chat, chat);
    }

    public void EnableChat(string sessionName) 
    {
        _sessionName = sessionName;
        _content.parent.parent.gameObject.SetActive(true);
    }
}
