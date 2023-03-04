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
        ItemsClickEvent();
    }

    private void ItemsClickEvent()
    {
        int groupIndex = 0;
        itemXMList.ForEach((itemGroup) =>
        {
            int itemIndex = 0;
            itemGroup.ForEach((item) =>
            {

                #region 궁금한거
                //item.RegisterCallback<ClickEvent>( e =>
                //{
                //    int gIdx = groupIndex;
                //    int iIdx = itemIndex;
                //    items[gIdx][iIdx].OnClick(e);
                //    ItemClickCheckEvent(e, gIdx, iIdx);
                //});// 이건 되는데
                   //item.RegisterCallback<ClickEvent>((evt) => {
                   //    Debug.Log("Evt : " + evt);
                   //    Debug.Log(items[groupIndex][itemIndex]);
                   //    items[groupIndex][itemIndex].OnClick(evt);
                   //}); // 이건 안되어요  이거 원인만 알면 밑에있는것 도 고칠 수 있을거 같습니다.
                #endregion

                #region 첫번째했던 방식
                //item.RegisterCallback<ClickEvent>(items[groupIndex][itemIndex].OnClick);
                //item.RegisterCallback<ClickEvent>((evt) =>
                //{
                //    Debug.Log("Groupindex : " + groupIndex);
                //    Debug.Log("Itemindex : " + itemIndex);
                //    items[groupIndex][itemIndex].OnItemClick += () =>
                //    {
                //        if (itemXMList[groupIndex][itemIndex].ClassListContains("select") == false)
                //        {
                //            itemXMList[groupIndex][itemIndex].AddToClassList("select");
                //        }
                //        Debug.Log(itemXMList[groupIndex][itemIndex]);
                //    };

                //    items[groupIndex][itemIndex].OnItemDoubleClick += () =>
                //    {
                //        if (itemXMList[groupIndex][itemIndex].ClassListContains("select"))
                //            itemXMList[groupIndex][itemIndex].RemoveFromClassList("select");
                //    };
                //ItemClickCheckEvent(evt);

                //});
                #endregion

                #region ClickHandler? 이것도 뭔가 잘못한듯 합니다.
                EventCallback<ClickEvent> clickHandler = items[groupIndex][itemIndex].OnClick;
                clickHandler += ItemClickCheckEvent;
                item.RegisterCallback(clickHandler);
                #endregion;

                void ItemClickCheckEvent(ClickEvent click)
                {
                    Debug.Log("Groupindex : " + groupIndex);
                    Debug.Log("Itemindex : " + itemIndex);
                    items[groupIndex][itemIndex].OnItemClick += () =>
                    {
                        if (itemXMList[groupIndex][itemIndex].ClassListContains("select") == false)
                        {
                            itemXMList[groupIndex][itemIndex].AddToClassList("select");
                        }
                        Debug.Log(itemXMList[groupIndex][itemIndex]);
                    };

                    items[groupIndex][itemIndex].OnItemDoubleClick += () =>
                    {
                        if (itemXMList[groupIndex][itemIndex].ClassListContains("select"))
                            itemXMList[groupIndex][itemIndex].RemoveFromClassList("select");
                    };
                }
                
                itemIndex++;
            });
            groupIndex++;
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
