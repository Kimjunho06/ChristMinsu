using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySpace : MonoBehaviour
{
    VisualElement root;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
    }
}
