using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomizeListController
{
    Label nameLabel;

    public void SetVisualElement(VisualElement visualElement)
    {
        nameLabel = visualElement.Q<Label>("item-name");
    }

    public void SetCharacterData(ItemSO itemData)
    {
        nameLabel.text = itemData.name;
    }
}
