using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour, IDragHandler
{
    public static event Action<PointerEventData> PointerDragged;

    public void OnDrag(PointerEventData pointerEventData)
    {
        PointerDragged?.Invoke(pointerEventData);
    }
}
