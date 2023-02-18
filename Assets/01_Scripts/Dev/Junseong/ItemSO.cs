using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum ItemType
{
    Head,
    Cloth,
    Accessari
}

[CreateAssetMenu(menuName = "Assets/SO/Item")]
public class ItemSO : ScriptableObject
{
    public ItemType _itemType;
    public string _itemName;
    public Image _itmeImage;
}