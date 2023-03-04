using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemListController
{
    public static ItemListController Instance { get; private set; }

    VisualTreeAsset itemPanelTemplate;

    #region �������� ���δ� �ڽ���
    GroupBox head_GroupBox;
    GroupBox Middle_GroupBox;
    GroupBox Bottom_GroupBox;
    #endregion
    #region item���� list��


    List<ItemSO> head_Items = new List<ItemSO>();
    List<ItemSO> cloth_Items = new List<ItemSO>();
    List<ItemSO> accessories_Items = new List<ItemSO>();
    

    List<List<Item>> items = new List<List<Item>>(); // 0��°�� �Ӹ�, 1��°�� ��, 2��°�� �Ǽ��縮
    List<Item> head_items = new List<Item>();
    List<Item> middle_items = new List<Item>();
    List<Item> bottom_items = new List<Item>();


    List<List<VisualElement>> itemXMList = new List<List<VisualElement>>(); //���� ����
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

                #region �ñ��Ѱ�
                //item.RegisterCallback<ClickEvent>( e =>
                //{
                //    int gIdx = groupIndex;
                //    int iIdx = itemIndex;
                //    items[gIdx][iIdx].OnClick(e);
                //    ItemClickCheckEvent(e, gIdx, iIdx);
                //});// �̰� �Ǵµ�
                   //item.RegisterCallback<ClickEvent>((evt) => {
                   //    Debug.Log("Evt : " + evt);
                   //    Debug.Log(items[groupIndex][itemIndex]);
                   //    items[groupIndex][itemIndex].OnClick(evt);
                   //}); // �̰� �ȵǾ��  �̰� ���θ� �˸� �ؿ��ִ°� �� ��ĥ �� ������ �����ϴ�.
                #endregion

                #region ù��°�ߴ� ���
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

                #region ClickHandler? �̰͵� ���� �߸��ѵ� �մϴ�.
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
    
    private void FillLists(List<ItemSO> list, string listFileName) // �����̸��� ���� ResourceFloder�ȿ� �ִ� �����۵��� ������
    {
        if (list.Count != 0)
        {
            Debug.Log("�̹� ����Ʈ�� ä��������");
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

            //�ٲܲ� ���� ��������� ��
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
