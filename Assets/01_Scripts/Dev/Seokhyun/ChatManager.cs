using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    private static ChatManager _instance;
    public static ChatManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = FindObjectOfType<ChatManager>();
            return _instance;
        }
    }


}
