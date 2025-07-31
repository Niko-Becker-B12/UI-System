using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Layout/UiCombi Layout Group")]
public class UiCombiLayoutGroup : LayoutGroup
{
    public enum FlexDirection { Row, Column }
    public enum JustifyContent { FlexStart, Center, FlexEnd, SpaceBetween, SpaceAround }
    public enum AlignItems { FlexStart, Center, FlexEnd, Stretch }
    public enum FlexWrap { NoWrap, Wrap }

    [SerializeField] public FlexDirection flexDirection = FlexDirection.Row;
    [SerializeField] public JustifyContent justifyContent = JustifyContent.FlexStart;
    [SerializeField] public AlignItems alignItems = AlignItems.Stretch;
    [SerializeField] public FlexWrap flexWrap = FlexWrap.NoWrap;

    [SerializeField] public float gapMain = 0f;
    [SerializeField] public float gapCross = 0f;

    [SerializeField] public bool forceStretch = false;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        CalcAlongAxis(0);
    }

    public override void CalculateLayoutInputVertical()
    {
        CalcAlongAxis(1);
    }

    public override void SetLayoutHorizontal()
    {
        SetChildrenAlongAxis(0);
    }

    public override void SetLayoutVertical()
    {
        SetChildrenAlongAxis(1);
    }

    private void CalcAlongAxis(int axis)
    {
        float totalPadding = (axis == 0) ? padding.horizontal : padding.vertical;
        float preferred = totalPadding;
        float flexible = 0;

        // We must calculate the preferred size **including gaps only between children**

        // For wrapping, we need to group children into lines and sum lines separately
        if (flexWrap == FlexWrap.Wrap)
        {
            float containerMain = rectTransform.rect.size[axis] - totalPadding;
            float lineMain = 0f;
            float lineCross = 0f;

            float maxCross = 0f;
            float totalCross = 0f;

            int itemsInLine = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                var child = rectChildren[i];
                float childMain = LayoutUtility.GetPreferredSize(child, axis);
                float childCross = LayoutUtility.GetPreferredSize(child, 1 - axis);
                float gap = (itemsInLine > 0) ? gapMain : 0f;

                if (itemsInLine > 0 && lineMain + gap + childMain > containerMain)
                {
                    // finish current line
                    totalCross += maxCross + (totalCross > 0 ? gapCross : 0);
                    maxCross = 0f;
                    lineMain = 0f;
                    itemsInLine = 0;
                }

                lineMain += gap + childMain;
                maxCross = Mathf.Max(maxCross, childCross);
                itemsInLine++;
            }

            // add last line cross size
            totalCross += maxCross;

            // total preferred is the max lineMain width or height + padding for main axis
            // and sum of line crosses + gaps + padding for cross axis
            if (axis == (int)flexDirection)
            {
                // Main axis: width or height depending on flexDirection
                // We must find max line main size for wrapping — but current code doesn't store max line width
                // For simplicity, we'll return the container size as preferred here (not perfect, but enough for sizing content)
                preferred = Mathf.Max(preferred, containerMain + totalPadding);
            }
            else
            {
                // Cross axis: sum of line heights or widths + padding + gapsCross
                preferred += totalCross;
            }
        }
        else
        {
            // No wrap, just sum all children main sizes + gaps between + padding

            int childCount = rectChildren.Count;

            for (int i = 0; i < childCount; i++)
            {
                var child = rectChildren[i];
                float pref = LayoutUtility.GetPreferredSize(child, axis);
                preferred += pref;

                if (i < childCount - 1)
                    preferred += gapMain;

                flexible += LayoutUtility.GetFlexibleSize(child, axis);
            }
        }

        SetLayoutInputForAxis(totalPadding, preferred, flexible, axis);
    }

    private void SetChildrenAlongAxis(int axis)
    {
        bool isMainHorizontal = flexDirection == FlexDirection.Row;
        if ((axis == 0 && !isMainHorizontal) || (axis == 1 && isMainHorizontal))
        {
            float size = rectTransform.rect.size[axis] - ((axis == 0) ? padding.horizontal : padding.vertical);
            foreach (var child in rectChildren)
            {
                SetChildAlongAxis(child, axis, (axis == 0) ? padding.left : padding.top, size);
            }
            return;
        }

        float containerMain = rectTransform.rect.size[axis] - ((axis == 0) ? padding.horizontal : padding.vertical);
        float posCross = (axis == 0) ? padding.top : padding.left;

        List<List<RectTransform>> lines = new();
        List<RectTransform> currentLine = new();
        float lineMain = 0f;
        float lineCross = 0f;

        foreach (var child in rectChildren)
        {
            float childMain = LayoutUtility.GetPreferredSize(child, axis);
            float childCross = LayoutUtility.GetPreferredSize(child, 1 - axis);
            float extra = currentLine.Count > 0 ? gapMain : 0;

            if (flexWrap == FlexWrap.Wrap && currentLine.Count > 0 && lineMain + extra + childMain > containerMain)
            {
                lines.Add(currentLine);
                posCross += lineCross + gapCross;
                currentLine = new List<RectTransform>();
                lineMain = 0;
                lineCross = 0;
                extra = 0;
            }

            currentLine.Add(child);
            lineMain += childMain + extra;
            lineCross = Mathf.Max(lineCross, childCross);
        }

        if (currentLine.Count > 0)
            lines.Add(currentLine);

        foreach (var line in lines)
        {
            float totalMain = 0f;
            List<float> childSizes = new();

            foreach (var child in line)
            {
                float size = LayoutUtility.GetPreferredSize(child, axis);
                childSizes.Add(size);
                totalMain += size;
            }

            float spacing = gapMain;
            float offset = (axis == 0) ? padding.left : padding.top;

            float extraSpace = containerMain - totalMain - gapMain * (line.Count - 1);
            switch (justifyContent)
            {
                case JustifyContent.Center:
                    offset += extraSpace / 2f;
                    break;
                case JustifyContent.FlexEnd:
                    offset += extraSpace;
                    break;
                case JustifyContent.SpaceBetween:
                    spacing = (line.Count > 1) ? extraSpace / (line.Count - 1) : 0f;
                    break;
                case JustifyContent.SpaceAround:
                    spacing = (line.Count > 0) ? extraSpace / line.Count : 0f;
                    offset += spacing / 2f;
                    break;
            }

            for (int i = 0; i < line.Count; i++)
            {
                var child = line[i];
                float childMain = childSizes[i];
                float childCross = LayoutUtility.GetPreferredSize(child, 1 - axis);

                if (forceStretch)
                    childMain = (containerMain - gapMain * (line.Count - 1)) / line.Count;

                if (alignItems == AlignItems.Stretch)
                    childCross = lineCross;

                float crossStart = posCross;
                if (alignItems != AlignItems.Stretch)
                {
                    switch (alignItems)
                    {
                        case AlignItems.Center:
                            crossStart += (lineCross - childCross) / 2f;
                            break;
                        case AlignItems.FlexEnd:
                            crossStart += (lineCross - childCross);
                            break;
                    }
                }

                if (axis == 0)
                {
                    SetChildAlongAxis(child, 0, offset, childMain);
                    SetChildAlongAxis(child, 1, crossStart, childCross);
                }
                else
                {
                    SetChildAlongAxis(child, 1, offset, childMain);
                    SetChildAlongAxis(child, 0, crossStart, childCross);
                }

                offset += childMain;
                if (i < line.Count - 1)
                    offset += spacing;
            }

            posCross += lineCross + gapCross;
        }
    }
}
