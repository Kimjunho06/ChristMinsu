using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item
{
    private VisualElement _itemroot;

    private VisualElement _profileImage;
    
    public Item(VisualElement _root)
    {
        _itemroot = _root;

        _profileImage = _root.Q<VisualElement>("Image");
    }

    public void OnClick(ClickEvent click)
    {
        //item click시 교체할거 구현해야함
        Debug.Log("ItemClick됨");
    }
}
