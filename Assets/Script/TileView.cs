using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class TileView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
    }
}