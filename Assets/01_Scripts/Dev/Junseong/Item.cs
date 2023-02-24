using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item
{
    private VisualElement _itemroot;

    private VisualElement _profileImage;
    private Sprite _sprite;
    

    public Item(VisualElement _root)
    {
        _itemroot = _root;
        
        //_sprite = sprite;
    }
}
