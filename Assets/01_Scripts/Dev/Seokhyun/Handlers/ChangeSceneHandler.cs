using ChristMinsu.Packet;
using Google.Protobuf;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneHandler : IPacketHandler
{
    public void Process(IMessage packet)
    {
        ChangeScene scene = packet as ChangeScene;
        SceneManager.LoadScene(scene.Name);
        Debug.Log(scene.Name);
    }
}