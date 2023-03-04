using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestUI : MonoBehaviour
{
    private UIDocument _document;
    private void Awake()
    {
        _document = GetComponent<UIDocument>();       
    }

    private void OnEnable()
    {
        VisualElement root =  _document.rootVisualElement;

        var c = root.Q<VisualElement>("Container");

        c.RegisterCallback<ClickEvent>(evt =>
       {
           Debug.Log( (evt.target as VisualElement).viewDataKey );
       });
    }
}
