using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class UIWindowDragable : UiWindow, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    //public DragableWindowDataObject dragableWindowDataObject;

    public bool isDraggable = false;

    public List<DragableDockingPositions> dockingPositions = new List<DragableDockingPositions>();


    private void Awake()
    {

        base.Awake();

    }

    public void ApplySkinData()
    {

        base.ApplySkinData();

    }

    private void Start()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {

        if(isDraggable)
            backgroundGraphic.rectTransform.anchoredPosition += eventData.delta / FindAnyObjectByType<Canvas>().scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        canvasGroup.alpha = 1f;

        if (dockingPositions.Count > 0)
        {

            for(int i = 0; i < dockingPositions.Count; i++)
            {

                if (dockingPositions[i].CanDock(backgroundGraphic.rectTransform.anchoredPosition))
                {

                    backgroundGraphic.rectTransform.anchoredPosition = dockingPositions[i].dockedPosition;

                    break;

                }
                else
                    continue;

            }

        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if(isDraggable)
            canvasGroup.alpha = .25f;

    }
}

[System.Serializable]
public class DragableDockingPositions
{

    public Vector2 dockedPosition;

    public Vector2 dockedXAxis;
    public Vector2 dockedYAxis;


    public bool CanDock(Vector2 position)
    {

        if(position.x >= dockedXAxis.x && position.x <= dockedXAxis.y)
        {

            if (position.y >= dockedYAxis.x && position.y <= dockedYAxis.y)
                return true;

        }

        return false;

    }

}