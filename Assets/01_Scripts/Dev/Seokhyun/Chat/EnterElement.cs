using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnterElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;

    public void SetText(string name)
    {
        _name.text = name;
    }
}
