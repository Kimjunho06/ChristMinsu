using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item
{
    public event Action OnItemDoubleClick;
    public event Action OnItemClick;

    private VisualElement _itemroot;

    private ItemType Type;
    private Sprite _sprite;
    CustomizeUIController _controller;

    public Item(VisualElement _root, ItemSO itemSO, CustomizeUIController controller)
    {
        _itemroot = _root;
        Type = itemSO._itemType;
        _sprite = itemSO._itmeImage;
        _controller = controller;
    }


    public void OnClick(ClickEvent click)
    {
        Debug.Log(_controller);
        switch (Type)
        {
            case ItemType.Head:
                
                if (_controller.playerCustomize.CurrentHeadSprite == _sprite)
                {
                    OnItemDoubleClick?.Invoke();
                    _controller.playerCustomize.CurrentHeadSprite = null;
                }
                else
                {
                    OnItemClick?.Invoke();
                    _controller.playerCustomize.CurrentHeadSprite = _sprite;
                }
                
                break;

            case ItemType.Cloth:
                
                if(_controller.playerCustomize.CurrentBodySprite == _sprite)
                {
                    OnItemDoubleClick?.Invoke();
                    _controller.playerCustomize.CurrentBodySprite = null;
                }
                else
                {
                    OnItemClick?.Invoke();
                    _controller.playerCustomize.CurrentBodySprite = _sprite;
                }
                
                break;

            case ItemType.Accessorie:

                if(_controller.playerCustomize.CurrentAccessariSprite == _sprite)
                {
                    OnItemDoubleClick?.Invoke();
                    _controller.playerCustomize.CurrentAccessariSprite = null;
                }
                else
                {
                    OnItemClick?.Invoke();
                    _controller.playerCustomize.CurrentAccessariSprite = _sprite;
                }
                
                break;
        }
        //item click�� ��ü�Ұ� �����ؾ���
        Debug.Log($"itemType : {Type}");
        Debug.Log($"itemName : {_sprite.name}");
    }
}
