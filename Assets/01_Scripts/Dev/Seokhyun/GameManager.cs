using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChristMinsu.DevSeok
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private string _connectionUrl;

        public static GameManager Instance;

        private Transform _socketInfoCanvas;
        public Transform SocketInfoCanvas
        {
            get
            {
                if(_socketInfoCanvas == null)
                    _socketInfoCanvas = transform.Find("Canvas");
                return _socketInfoCanvas;
            }
        }

        private void Awake()
        {
            if (Instance != null)
                Debug.LogError("Multiple GameManager is running!");

            Instance = this;
            DontDestroyOnLoad(gameObject);

            NetworkManager.Instance = gameObject.AddComponent<NetworkManager>();
            NetworkManager.Instance.Init(_connectionUrl, SocketInfoCanvas);
            NetworkManager.Instance.Connection();
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