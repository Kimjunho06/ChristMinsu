using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemListController
{
    public static ItemListController Instance { get; private set; }

    VisualTreeAsset itemPanelTemplate;

    #region 아이템을 감싸는 박스들
    GroupBox head_GroupBox;
    GroupBox Middle_GroupBox;
    GroupBox Bottom_GroupBox;
    #endregion
    #region item관련 list들


    List<ItemSO> head_Items = new List<ItemSO>();
    List<ItemSO> cloth_Items = new List<ItemSO>();
    List<ItemSO> accessories_Items = new List<ItemSO>();
    

    List<List<Item>> items = new List<List<Item>>(); // 0번째가 머리, 1번째가 옷, 2번째가 악세사리
    List<Item> head_items = new List<Item>();
    List<Item> middle_items = new List<Item>();
    List<Item> bottom_items = new List<Item>();


    List<List<VisualElement>> itemXMList = new List<List<VisualElement>>(); //위와 동일
    List<VisualElement> head_ItemXMList = new List<VisualElement>();
    List<VisualElement> middle_ItemXMList = new List<VisualElement>();
    List<VisualElement> bottom_ItemXMList = new List<VisualElement>();


    #endregion
    CustomizeUIController _CustomizeController;

    public void InitializeItemList(VisualElement root ,VisualTreeAsset itemPanelElement, CustomizeUIController controller)
    {
        EnumerateAllItems();
        ListAssign();
        FindElement(root);

        itemPanelTemplate = itemPanelElement;
        _CustomizeController = controller;

        FillItemList(root, head_Items, head_GroupBox.Q<GroupBox>("head--GroupBox"), 0);
        FillItemList(root, cloth_Items, Middle_GroupBox.Q<GroupBox>("middle--GroupBox"), 1);
        FillItemList(root, accessories_Items, Bottom_GroupBox.Q<GroupBox>("bottom--GroupBox"), 2);

        int Groupindex = 0;
        itemXMList.ForEach((itemGroup) =>
        {
            int itemIndex = 0;
            itemGroup.ForEach((item) =>
            {
                item.RegisterCallback<ClickEvent>(items[Groupindex][itemIndex].OnClick); //각각 상응하는 인덱스에 아이템 클래스에 ONClick 메서드 호출

                //items[Groupindex][itemIndex].OnItemClick += () =>
                //{
                    
                //};

                //items[Groupindex][itemIndex].OnItemDoubleClick += () =>
                //{
                //    if (itemXMList[Groupindex][itemIndex].ClassListContains("select"))
                //    itemXMList[Groupindex][itemIndex].RemoveFromClassList("select");
                //};

                itemIndex++;    
            });
            Groupindex++;
        });
    }

    private void FindElement(VisualElement root)
    {
        head_GroupBox = root.Q<GroupBox>("Top-Group");
        Middle_GroupBox = root.Q<GroupBox>("Middle-Group");
        Bottom_GroupBox = root.Q<GroupBox>("Bottom-Group");
    }

    private void ListAssign()
    {
        items.Add(head_items);
        items.Add(middle_items);
        items.Add(bottom_items);

        itemXMList.Add(head_ItemXMList);
        itemXMList.Add(middle_ItemXMList);
        itemXMList.Add(bottom_ItemXMList);
    }


    private void EnumerateAllItems()
    {
        FillLists(head_Items, "Head_Item");
        FillLists(cloth_Items, "Cloth_Item");
        FillLists(accessories_Items, "Accessorie_Item");
    }
    
    private void FillLists(List<ItemSO> list, string listFileName) // 파일이름을 통해 ResourceFloder안에 있는 아이템들을 가져옴
    {
        if (list.Count != 0)
        {
            Debug.Log("이미 리스트가 채워져있음");
            return;
        }
        list.AddRange(Resources.LoadAll<ItemSO>(listFileName));
    }


    private void FillItemList(VisualElement root, List<ItemSO> list, GroupBox group , int groupIndex)
    {
        int itemCnt = 0;
        list.ForEach(itemSO =>
        {

            Item item = new Item(root, itemSO, _CustomizeController);
            VisualElement itemXML = itemPanelTemplate.Instantiate().Q("Item");
            Label label = itemXML.Q<Label>("Text");

            //바꿀꺼 여기 적어놓으면 됨
            {
                label.text = (itemCnt + 1).ToString();
                itemXML.style.backgroundImage = new StyleBackground(itemSO._itmeImage);
            }

            //Debug.Log(items[groupIndex]);
            items[groupIndex].Add(item);
            itemXMList[groupIndex].Add(itemXML);
            group.Add(itemXML);
            
            itemCnt++;
            
        });
        
    }

}
