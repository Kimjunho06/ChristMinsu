using ChristMinsu.Packet;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChristMinsu.DevSeok
{
    public class GetNameUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _field;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => {
                Name name = new Name { Value = _field.text };
                NetworkManager.Instance.RegisterSend((ushort)MSGID.Name, name);
            });
        }
    }
}
