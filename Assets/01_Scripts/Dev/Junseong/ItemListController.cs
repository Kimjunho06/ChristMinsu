using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemListController
{
    public static ItemListController Instance { get; private set; }

    VisualTreeAsset itemPanelTemplate;

    GroupBox head_GroupBox;
    GroupBox Middle_GroupBox;
    GroupBox Under_GroupBox;

    public void InitializeItemList(VisualElement root ,VisualTreeAsset itemPanelElement)
    {
        head_GroupBox = root.Q<GroupBox>("Top-Group");
        Middle_GroupBox = root.Q<GroupBox>("Middle-Group");
        Under_GroupBox = root.Q<GroupBox>("Under-Group");

        EnumerateAllItems();
        itemPanelTemplate = itemPanelElement;
        FillItemList();

    }

    List<List<ItemSO>> Allitems;
    List<ItemSO> head_Items = new List<ItemSO>();
    List<ItemSO> cloth_Items = new List<ItemSO>();
    List<ItemSO> accessories_Items = new List<ItemSO>();

    private void EnumerateAllItems()
    {
        FillLists(head_Items, "Head_Item");
        FillLists(cloth_Items, "Cloth_Item");
        FillLists(accessories_Items, "Accessorie_Item");
    }
    
    private void FillLists(List<ItemSO> list, string listFileName) // 파일이름을 통해 ResourceFloder안에 있는 아이템들을 가져옴
    {
        list.AddRange(Resources.LoadAll<ItemSO>(listFileName));
    }

    private void FillItemList(List<ItemSO> list)
    {
        
        
    }

}
