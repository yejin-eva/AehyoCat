using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<DragComponent> endDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("being drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endDrag?.Invoke(this);
        Debug.Log("end drag");
    }

}
