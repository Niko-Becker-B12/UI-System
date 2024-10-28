using UnityEngine;
using UnityEngine.EventSystems;

public class UiDragable : UiElement, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    RectTransform parent => this.transform.parent.GetComponent<RectTransform>();

    public void OnDrag(PointerEventData eventData)
    {

        rectTransform.localPosition = eventData.position;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        this.transform.parent = parent;

    }

}