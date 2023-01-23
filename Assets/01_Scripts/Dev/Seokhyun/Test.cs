using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Seokhyun
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private TMP_InputField field;

        public void SendName()
        {
            if(field.text == null || field.text == "") return;
            NetworkManager.Instance.SendName(field.text);
        }
    }
}