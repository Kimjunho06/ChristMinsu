using System;
using System.Collections;
using System.Collections.Generic;
using ChristMinsu.Packet;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace ChristMinsu.DevSeok
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private string _connectionUrl;

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance != null)
                Debug.LogError("Multiple GameManager is running!");

            Instance = this;
            DontDestroyOnLoad(gameObject);

            NetworkManager.Instance = gameObject.AddComponent<NetworkManager>();
            NetworkManager.Instance.Init(_connectionUrl);
            NetworkManager.Instance.Connection();
        }

        public void CreateRemoteManager(SessionInfo[] allSessionInfo)
        {
            RemotePlayerManager.Instance = gameObject.AddComponent<RemotePlayerManager>();
            RemotePlayerManager.Instance.Init(allSessionInfo);
        }

        public void KillComponent<T>() where T : MonoBehaviour
        {
            if(TryGetComponent<T>(out T component))
            {
                Destroy(component);
            }
        }

        private void OnDestroy()
        {
            NetworkManager.Instance.Disconnect();
        }

        //Debug code
        private void Update()
        {

        }
    }

}