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
    public PlayerCustomize playerCustomize { get; private set; }

    List<VisualElement> itemGroupList;

    List<Button> LeftMoveButton;
    List<Button> RightMoveButton;

    VisualElement CustomizeHead;
    VisualElement CustomizeCloth;
    VisualElement CustomizeAccessori;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        Init();
        ButtonEvent();
    }


    private void Init()
    {
        trScene = GetComponent<SceneTrans>();
        
        if(playerCustomize == null)
        {
            playerCustomize = new PlayerCustomize();
        }
        if(itemController == null)
        {
            itemController = new ItemListController();
        }
        //Debug.Log("ItemCOntroller : " + itemController);
        itemController.InitializeItemList(root, itemPanelTemplate, this);
        
        LeftMoveButton = root.Query<Button>(className : "leftButton").ToList();
        RightMoveButton = root.Query<Button>(className : "rightButton").ToList();
        itemGroupList = root.Query<VisualElement>(className: "item--Group").ToList();

        CustomizeHead = root.Q<VisualElement>("CustomizeHead");
        CustomizeCloth = root.Q<VisualElement>("CustomizeMiddle");
        CustomizeAccessori = root.Q<VisualElement>("CusomizeAccessari");
       
        playerCustomize.OnChanged += ChangeSprite;
    }

    private void ChangeSprite()
    {
        CustomizeHead.style.backgroundImage = new StyleBackground(playerCustomize.CurrentHeadSprite);
        CustomizeCloth.style.backgroundImage = new StyleBackground(playerCustomize.CurrentBodySprite);
        CustomizeAccessori.style.backgroundImage = new StyleBackground(playerCustomize.CurrentAccessariSprite);
    }

    private void ButtonEvent()
    {
        SceneTransButton();
        ItemMoveButton();
    }

    private void ItemMoveButton()
    {
        float FirstXPos;
        float LastXPos;

        LeftMoveButton.ForEach(leftButton => // LeftMoveButton 은 VisualElement를 담는 리스트
        {
            leftButton.RegisterCallback<ClickEvent>(evt =>
            {
                VisualElement groupBox = leftButton.parent.Q<GroupBox>(className : "item--Group");
                List<VisualElement> items = groupBox.Query<VisualElement>(className : "item").ToList();

                float xPos = items[0].worldBound.position.x;// 제일 앞에 배치된 아이템의 x
                FirstXPos = leftButton.worldBound.position.x;

                if(xPos < FirstXPos)
                {
                    items.ForEach(item =>
                    {
                        item.transform.position = 
                            new Vector3(item.transform.position.x + (groupBox.resolvedStyle.width), item.transform.position.y, item.transform.position.z);
                    });
                }
            });
        });

        RightMoveButton.ForEach(rightButton =>
        {
            rightButton.RegisterCallback<ClickEvent>(evt =>
            {
                VisualElement groupBox = rightButton.parent.Q<GroupBox>(className: "item--Group");
                List<VisualElement> items = groupBox.Query<VisualElement>(className: "item").ToList();
                
                float xPos = items[items.Count-1].worldBound.position.x; // 제일 뒤에 배치된 아이템의 x
                LastXPos = rightButton.worldBound.position.x;
                Debug.Log("xPos : " + xPos);
                Debug.Log("LastPos : " + LastXPos);

                if(xPos > LastXPos)
                {
                    items.ForEach(item =>
                    {
                        item.transform.position =
                            new Vector3(item.transform.position.x - (groupBox.resolvedStyle.width), item.transform.position.y, item.transform.position.z);
                    });
                }
            });
        });
    }

    private void SceneTransButton()
    {
        Button backbtn = root.Q<Button>("back--button");
        Button joinbtn = root.Q<Button>("join--button");

        backbtn.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(DelayCoroutine(1, () =>
            {
                //trScene.TranScene("{채널 선택 씬 이름}");
                Debug.LogError("이동하려는 씬이 존재하지 않음");
            }));
        });

        joinbtn.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(DelayCoroutine(1, () =>
            {
                //trScene.TranScene("{인 게임 씬 이름}");
                Debug.LogError("이동하려는 씬이 존재하지 않음");
            }));
        });
    }

    IEnumerator DelayCoroutine(float t, Action action)
    {
        yield return new WaitForSeconds(t);
        action();
    }
}
