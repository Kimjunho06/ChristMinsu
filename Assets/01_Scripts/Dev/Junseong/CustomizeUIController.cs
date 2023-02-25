using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;

public class CustomizeUIController : MonoBehaviour
{
    VisualElement root;
   
    [SerializeField]
    VisualTreeAsset itemPanelTemplate;

    SceneTrans trScene;
    ItemListController itemController;

    List<VisualElement> itemGroupList;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        ButtonEvent();
    }

    private void Init()
    {
        trScene = GetComponent<SceneTrans>();
        itemGroupList = root.Query<VisualElement>(className: "item--Group").ToList();
        
        if(itemController == null)
        {
            itemController = new ItemListController();
        }
        Debug.Log("ItemCOntroller : " + itemController);
        itemController.InitializeItemList(root, itemPanelTemplate);
    }

    private void ButtonEvent()
    {
        Button backbtn = root.Q<Button>("back--button");
        Button joinbtn = root.Q<Button>("join--button");

        backbtn.RegisterCallback<ClickEvent>(evt =>
        {
            Debug.LogError("이동하려는 씬이 존재하지 않음");
            //trScene.TranScene("{채널 선택 씬 이름}");
        });

        joinbtn.RegisterCallback<ClickEvent>(evt =>
        {
            Debug.LogError("이동하려는 씬이 존재하지 않음");
            //trScene.TranScene("{인 게임 씬 이름}");
        });
    }
}
