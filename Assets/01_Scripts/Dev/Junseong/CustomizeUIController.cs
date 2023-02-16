using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class CustomizeUIController : MonoBehaviour
{

    List<Button> itemPanels_Head;
    List<Button> itemPanels_Cloth;
    List<Button> itemPanels_Accessories;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
