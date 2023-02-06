using ChristMinsu.Packet;
using Google.Protobuf;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChristMinsu.DevSeok;

public class ChangeSceneHandler : IPacketHandler
{
    public void Process(IMessage packet)
    {
        ChangeScene scene = packet as ChangeScene;
        SceneManager.LoadScene(scene.SceneName);
        ChatManager.Instance.EnableChat(scene.SessionName);
        
        Debug.Log(scene.SceneName);
    }
}