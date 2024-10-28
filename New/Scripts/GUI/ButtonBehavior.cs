using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [Header("On Mouse Enter")]
    [Space]
    public List<Function> onMouseEnter = new List<Function>();

    [Header("On Mouse Down")]
    [Space]
    public List<Function> onMouseDown = new List<Function>();

    [Header("On Mouse Exit")]
    [Space]
    public List<Function> onMouseExit = new List<Function>();

    [Header("On Button Reset")]
    [Space]
    public List<Function> onButtonReset = new List<Function>();

    public bool clickableOnce = false;

    [ReadOnly]
    public bool wasClicked = false;

    bool isResetting = false;


    private void Start()
    {



    }

    public void OnMouseEnter()
    {

        if (!wasClicked)
            for (int i = 0; i < onMouseEnter.Count; i++)
            {

                Function.InvokeEvent(onMouseEnter[i], this);

            }

    }

    public void OnMouseDown()
    {

        if (clickableOnce)
        {

            if (!wasClicked)
            {

                for (int i = 0; i < onMouseDown.Count; i++)
                {

                    Function.InvokeEvent(onMouseDown[i], this);

                }

                wasClicked = true;

            }

        }
        else
        {

            for (int i = 0; i < onMouseDown.Count; i++)
            {

                Function.InvokeEvent(onMouseDown[i], this);

            }

        }

    }

    public void OnMouseExit()
    {
        if (!wasClicked)
            for (int i = 0; i < onMouseExit.Count; i++)
            {

                Function.InvokeEvent(onMouseExit[i], this);

            }

    }

    public void ResetClick()
    {

        wasClicked = false;

        if (!this.gameObject.activeSelf)
            return;

        if (!isResetting)
        {

            isResetting = true;

            for (int i = 0; i < onButtonReset.Count; i++)
            {

                Function.InvokeEvent(onButtonReset[i], this);

            }

            isResetting = false;

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        OnMouseDown();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        OnMouseEnter();

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        OnMouseExit();

    }

#if UNITY_EDITOR

    #region https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/Inspector/RectTransformEditor.cs#L24-L26
    private static Vector2 kShadowOffset = new Vector2(1, -1);
    private static Color kShadowColor = new Color(0, 0, 0, 0.5f);
    private const float kDottedLineSize = 5f;
    #endregion

    void OnDrawGizmos()
    {
        Image image = GetComponent<Image>();
        RectTransform gui = GetComponent<RectTransform>();

        if (image == null)
            return;

        #region https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/Inspector/RectTransformEditor.cs#L646-L660
        Rect rectInOwnSpace = gui.rect;
        // Rect rectInUserSpace = rectInOwnSpace;
        Rect rectInParentSpace = rectInOwnSpace;
        Transform ownSpace = gui.transform;
        // Transform userSpace = ownSpace;
        Transform parentSpace = ownSpace;
        RectTransform guiParent = null;
        if (ownSpace.parent != null)
        {
            parentSpace = ownSpace.parent;
            rectInParentSpace.x += ownSpace.localPosition.x;
            rectInParentSpace.y += ownSpace.localPosition.y;

            guiParent = parentSpace.GetComponent<RectTransform>();
        }
        #endregion

        // patSilva's post: https://forum.unity.com/threads/ui-image-component-raycast-padding-needs-a-gizmo.1019260/#post-6828020
        // The image.raycastPadding order of the Vector4 is:
        // X = Left
        // Y = Bottom
        // Z = Right
        // W = Top

        Rect paddingRect = new Rect(rectInParentSpace);
        paddingRect.xMin += image.raycastPadding.x;
        paddingRect.xMax -= image.raycastPadding.z;
        paddingRect.yMin += image.raycastPadding.y;
        paddingRect.yMax -= image.raycastPadding.w;

        // uncomment below line to show only when rect tool is active
        // if (Tools.current == Tool.Rect)
        {
            //change the color of the handles as you wish
            Handles.color = Color.green;
            DrawRect(paddingRect, parentSpace, true);
        }
    }

    #region https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/Inspector/RectTransformEditor.cs#L618-L638
    void DrawRect(Rect rect, Transform space, bool dotted)
    {
        Vector3 p0 = space.TransformPoint(new Vector2(rect.x, rect.y));
        Vector3 p1 = space.TransformPoint(new Vector2(rect.x, rect.yMax));
        Vector3 p2 = space.TransformPoint(new Vector2(rect.xMax, rect.yMax));
        Vector3 p3 = space.TransformPoint(new Vector2(rect.xMax, rect.y));
        if (!dotted)
        {
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p1, p2);
            Handles.DrawLine(p2, p3);
            Handles.DrawLine(p3, p0);
        }
        else
        {
            DrawDottedLineWithShadow(kShadowColor, kShadowOffset, p0, p1, kDottedLineSize);
            DrawDottedLineWithShadow(kShadowColor, kShadowOffset, p1, p2, kDottedLineSize);
            DrawDottedLineWithShadow(kShadowColor, kShadowOffset, p2, p3, kDottedLineSize);
            DrawDottedLineWithShadow(kShadowColor, kShadowOffset, p3, p0, kDottedLineSize);
        }
    }
    #endregion

    #region https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/Inspector/RectHandles.cs#L278-L296
    public static void DrawDottedLineWithShadow(Color shadowColor, Vector2 screenOffset, Vector3 p1, Vector3 p2, float screenSpaceSize)
    {
        Camera cam = Camera.current;
        if (!cam || Event.current.type != EventType.Repaint)
            return;

        Color oldColor = Handles.color;

        // shadow
        shadowColor.a = shadowColor.a * oldColor.a;
        Handles.color = shadowColor;
        Handles.DrawDottedLine(
            cam.ScreenToWorldPoint(cam.WorldToScreenPoint(p1) + (Vector3)screenOffset),
            cam.ScreenToWorldPoint(cam.WorldToScreenPoint(p2) + (Vector3)screenOffset), screenSpaceSize);

        // line itself
        Handles.color = oldColor;
        Handles.DrawDottedLine(p1, p2, screenSpaceSize);
    }
    #endregion

#endif

}