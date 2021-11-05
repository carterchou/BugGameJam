using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool isDrag;
    public ScrollRect scrollRect;
    public Vector2 pointerPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.OnBeginDrag(eventData);
        isDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        pointerPos = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.OnEndDrag(eventData);
        isDrag = false;
    }
}