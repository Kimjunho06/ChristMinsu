using System.Collections;
using System.Collections.Generic;
using ChristMinsu.Packet;
using UnityEngine;

public class RemotePlayerManager : MonoBehaviour
{
    public static RemotePlayerManager Instance = null;

    public List<RemotePlayer> remotePlayers = new List<RemotePlayer>();

    

    public void Init(SessionInfo[] allSessionInfo)
    {
        for(int i = 0; i < allSessionInfo.Length; i++)
        {
            
        }
    }
}
