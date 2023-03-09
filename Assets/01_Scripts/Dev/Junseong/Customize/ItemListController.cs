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

    List<GroupBox> itemGroupBoxs = new List<GroupBox>();

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
        FindElement(root);

        itemPanelTemplate = itemPanelElement;
        _CustomizeController = controller;

        ListAssign();

        FillItemList(root, head_Items, head_GroupBox.Q<GroupBox>("head--GroupBox"), 0);
        FillItemList(root, cloth_Items, Middle_GroupBox.Q<GroupBox>("middle--GroupBox"), 1);
        FillItemList(root, accessories_Items, Bottom_GroupBox.Q<GroupBox>("bottom--GroupBox"), 2);
        
        ItemsClickEvent();

        //ItemsClickEvent2();
    }

    private void ItemsClickEvent()
    {
        int groupIndex = 0;
        itemXMList.ForEach((itemGroup) =>
        {
            int itemIndex = 0;
            itemGroup.ForEach((item) =>
            {
                #region ClickHandler
                EventCallback<ClickEvent> clickHandler = items[groupIndex][itemIndex].OnClick;
                item.RegisterCallback(clickHandler);
                #endregion;

                itemIndex++;
            });
            groupIndex++;
        });

        itemGroupBoxs.ForEach((group) =>
        {
            VisualElement lastItem = null;
            group.RegisterCallback<ClickEvent>(evt =>
            {
                var currentItem = evt.target as VisualElement;

                Debug.Log($"currentItemd : {currentItem}");
                Debug.Log($"lastItemd : {lastItem}");
                
                if (currentItem.ClassListContains("select") == false)
                {
                    currentItem.AddToClassList("select");
                }
                else
                {
                    currentItem.RemoveFromClassList("select");
                }

                if (lastItem != currentItem) // 이전 선택 아이템이 현재선택 아이템과 다를때
                {
                    if(lastItem != null) // 이전에 선택되었던 아이템 select효과 지우기
                    {
                        if(lastItem.ClassListContains("select") == true)
                        {
                            lastItem.RemoveFromClassList("select");
                        }
                    }
                }

                lastItem = currentItem;
            
            });
        });

    }

    private void ItemsClickEvent2()
    {
        itemGroupBoxs.ForEach((group) =>
        {
            group.RegisterCallback<ClickEvent>(evt =>
            {
                var item = evt.target as VisualElement;
                
                if (item.ClassListContains("select") == false)
                    item.AddToClassList("select");
                else 
                    item.RemoveFromClassList("select");
            });
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

        itemGroupBoxs.Add(head_GroupBox.Q<GroupBox>("head--GroupBox"));
        itemGroupBoxs.Add(Middle_GroupBox.Q<GroupBox>("middle--GroupBox"));
        itemGroupBoxs.Add(Bottom_GroupBox.Q<GroupBox>("bottom--GroupBox"));
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
        //Debug.Log("Groupindex~~ : " + groupIndex);
        int itemIndex = 0;
        list.ForEach(itemSO =>
        {

            Item item = new Item(root, itemSO, _CustomizeController, itemIndex);
            VisualElement itemXML = itemPanelTemplate.Instantiate().Q("Item");
            Label label = itemXML.Q<Label>("Text");

            //바꿀꺼 여기 적어놓으면 됨
            {
                label.text = (itemIndex + 1).ToString();
                itemXML.style.backgroundImage = new StyleBackground(itemSO._itmeImage);
            }

            //Debug.Log(items[groupIndex]);
            items[groupIndex].Add(item);
            itemXMList[groupIndex].Add(itemXML);
            group.Add(itemXML);
            //Debug.Log("itemIndex : " + itemIndex);
            itemIndex++;
            
        });
        
    }

}
