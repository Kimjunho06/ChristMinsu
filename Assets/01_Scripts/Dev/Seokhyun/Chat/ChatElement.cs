using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name, _value;
    
    public void SetText(string name, string value)
    {
        _name.text = name;
        _value.text = value;
    }
}
