using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AlignItem : MonoBehaviour
{
    VisualElement root;

    [SerializeField]
    VisualTreeAsset itemSpaceAsset;

    [SerializeField] int oneLineMaxSpace = 6;

    int itemSpaceCnt = 18;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        VisualElement itemSpaceGroup = root.Q("Panel--middle");
        for(int i = 0; i < itemSpaceCnt; i++)
        {
            if(i % oneLineMaxSpace == 0 && i != 0)
            {
                VisualElement itemSpace = itemSpaceAsset.Instantiate();
                itemSpace.transform.position = new Vector3(transform.position.x, transform.position.y - 300, transform.position.z);
                itemSpaceGroup.Add(itemSpace.Q<Button>());
                continue;
            }
            itemSpaceGroup.Add(itemSpaceAsset.Instantiate().Q<Button>());      
        }
    }
}
