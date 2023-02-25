using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum ItemType
{
    Head,
    Cloth,
    Accessorie
}

[CreateAssetMenu(menuName = "Assets/SO/Item")]
public class ItemSO : ScriptableObject
{
    public ItemType _itemType;
    public string _itemName;
    public Sprite _itmeImage;
}
