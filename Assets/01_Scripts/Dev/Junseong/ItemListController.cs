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

        int Groupindex = 0;
        itemXMList.ForEach((itemGroup) =>
        {
            int itemIndex = 0;
            itemGroup.ForEach((item) =>
            {
                item.RegisterCallback<ClickEvent>(items[Groupindex][itemIndex].OnClick); //���� �����ϴ� �ε����� ������ Ŭ������ ONClick �޼��� ȣ��

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
        int itemCnt = 0;
        list.ForEach(itemSO =>
        {

            Item item = new Item(root, itemSO, _CustomizeController);
            VisualElement itemXML = itemPanelTemplate.Instantiate().Q("Item");
            Label label = itemXML.Q<Label>("Text");

            //�ٲܲ� ���� ��������� ��
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
