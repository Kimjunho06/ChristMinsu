using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    private RawImage miniMapViewImage;
    private RectTransform btnImage;

    private bool isOpen = true;

    private void Awake()
    {
        miniMapViewImage = GetComponentInChildren<RawImage>();
        btnImage = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Update()
    {
        MiniMapState(new Vector2(0, 350));
    }

    public void OnMiniMap()
    {
        isOpen = !isOpen;
    }

    private void MiniMapState(Vector2 openPos)
    {
        btnImage.anchoredPosition = isOpen ? openPos : Vector2.zero;
        miniMapViewImage.gameObject.SetActive(isOpen);
    }
}