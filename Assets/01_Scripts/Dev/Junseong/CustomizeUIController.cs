using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;

public class CustomizeUIController : MonoBehaviour
{
    VisualTreeAsset ListEntryTemplate;

    ListView head_ItemGroup;
    ListView cloth_ItemGroup;
    ListView accessories_ItemGroup;

    List<ListView> itemGroupList = new List<ListView>();

    Label ItemClassLabdl;
    Label ItemNameLabel;

    SceneTrans trScene;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        ButtonEvent(root);

        Init();
    }

    private void Init()
    {
        trScene = GetComponent<SceneTrans>();
        
        itemGroupList.Add(head_ItemGroup);
        itemGroupList.Add(cloth_ItemGroup);
        itemGroupList.Add(accessories_ItemGroup);
    }

    private void ButtonEvent(VisualElement root)
    {
        Button backbtn = root.Q<Button>("back--button");
        Button joinbtn = root.Q<Button>("join--button");

        backbtn.RegisterCallback<ClickEvent>(evt =>
        {
            trScene.TranScene("{채널 선택 씬 이름}");
        });

        joinbtn.RegisterCallback<ClickEvent>(evt =>
        {
            trScene.TranScene("{인 게임 씬 이름}");
        });
    }

    public void InitializeItemList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllItems();
        
        ListEntryTemplate = listElementTemplate;

        head_ItemGroup = root.Q<ListView>("head--GroupBox");
        cloth_ItemGroup = root.Q<ListView>("cloth--GroupBox");
        accessories_ItemGroup = root.Q<ListView>("accessories--GroupBox");

        FillItemList();

        foreach(ListView item in itemGroupList)
        {
            item.onSelectionChange += OnitemSelected;
        }
    }

    List<List<ItemSO>> Allitems;

    private void FillItemList()
    {
        Allitems = new List<List<ItemSO>>();

        for(int i = 0; i < Allitems.Count; i++)
        {
            Allitems[i] = new List<ItemSO>();
            Allitems[i].AddRange(Resources.LoadAll<ItemSO>($"itemList {i}"));
        }
    }

    private void EnumerateAllItems()
    {
        throw new NotImplementedException();
    }

    void OnitemSelected(IEnumerable<object> selectedItems)
    {
        // Get the currently selected item directly from the ListView
        var selectedItem = head_ItemGroup.selectedItem as ItemSO;

        // Handle none-selection (Escape to deselect everything)
        if (selectedItem == null)
        {
            // Clear
            //CharClassLabel.text = "";
            //CharNameLabel.text = "";
            //CharPortrait.style.backgroundImage = null;

            return;
        }

        // Fill in character details
        //CharClassLabel.text = selectedItem.Class.ToString();
        //CharNameLabel.text = selectedItem.CharacterName;
        //CharPortrait.style.backgroundImage = new StyleBackground(selectedItem.PortraitImage);
    }
}
