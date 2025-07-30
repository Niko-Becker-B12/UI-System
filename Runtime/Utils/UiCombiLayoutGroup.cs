using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using GPUI;

[AddComponentMenu("Layout/Ui Combi Layout Group")]
public class UiCombiLayoutGroup : LayoutGroup
{
    public enum FlexDirection
    {
        Row,
        RowReverse,
        Column,
        ColumnReverse,
        RowWrap,
        ColumnWrap
    }

    public enum JustifyContent
    {
        FlexStart,
        Center,
        FlexEnd,
        SpaceBetween,
        SpaceAround
    }

    public enum AlignItems
    {
        Stretch,
        FlexStart,
        Center,
        FlexEnd
    }

    public FlexDirection flexDirection = FlexDirection.RowWrap;
    public JustifyContent justifyContent = JustifyContent.FlexStart;
    public AlignItems alignItems = AlignItems.Stretch;

    public float spacingX = 0f;
    public float spacingY = 0f;

    private bool IsHorizontal => flexDirection == FlexDirection.Row ||
                                 flexDirection == FlexDirection.RowReverse ||
                                 flexDirection == FlexDirection.RowWrap;

    private bool IsWrap => flexDirection == FlexDirection.RowWrap ||
                           flexDirection == FlexDirection.ColumnWrap;

    private float _preferredMainSize = 0f;
    private float _preferredCrossSize = 0f;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        CalculateFlexLayoutSizes();
        SetLayoutInputForAxis(_preferredMainSize, _preferredMainSize, -1, 0);
    }

    public override void CalculateLayoutInputVertical()
    {
        CalculateFlexLayoutSizes();
        SetLayoutInputForAxis(_preferredCrossSize, _preferredCrossSize, -1, 1);
    }

    public override void SetLayoutHorizontal()
    {
        SetChildrenAlongAxis(0);
    }

    public override void SetLayoutVertical()
    {
        SetChildrenAlongAxis(1);
    }

    private void CalculateFlexLayoutSizes()
    {
        float containerWidth = rectTransform.rect.width - padding.horizontal;
        float containerHeight = rectTransform.rect.height - padding.vertical;

        float mainAvailable = IsHorizontal ? containerWidth : containerHeight;
        if (mainAvailable <= 0) mainAvailable = float.MaxValue; // Treat zero or negative as infinite for initial calc

        float spacingMain = IsHorizontal ? spacingX : spacingY;
        float spacingCross = IsHorizontal ? spacingY : spacingX;

        List<List<RectTransform>> lines = new List<List<RectTransform>>();
        List<float> lineMainSizes = new List<float>();
        List<float> lineCrossSizes = new List<float>();

        List<RectTransform> currentLine = new List<RectTransform>();
        float lineMain = 0f;
        float lineCross = 0f;

        float maxMain = 0f;
        float totalCross = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform child = rectChildren[i];
            float childMain = LayoutUtility.GetPreferredSize(child, IsHorizontal ? 0 : 1);
            float childCross = LayoutUtility.GetPreferredSize(child, IsHorizontal ? 1 : 0);

            // Spacing only between elements, so add spacing if not first in line
            float spacingToAdd = currentLine.Count > 0 ? spacingMain : 0f;

            bool needWrap = IsWrap && currentLine.Count > 0 && (lineMain + spacingToAdd + childMain > mainAvailable);

            if (needWrap)
            {
                // Save current line
                lines.Add(currentLine);
                lineMainSizes.Add(lineMain);
                lineCrossSizes.Add(lineCross);

                totalCross += lineCross + spacingCross;
                maxMain = Mathf.Max(maxMain, lineMain);

                // Start new line
                currentLine = new List<RectTransform>();
                lineMain = 0f;
                lineCross = 0f;
                spacingToAdd = 0f;
            }

            lineMain += spacingToAdd + childMain;
            lineCross = Mathf.Max(lineCross, childCross);
            currentLine.Add(child);
        }

        // Add last line
        if (currentLine.Count > 0)
        {
            lines.Add(currentLine);
            lineMainSizes.Add(lineMain);
            lineCrossSizes.Add(lineCross);

            totalCross += lineCross;
            maxMain = Mathf.Max(maxMain, lineMain);
        }

        // Calculate preferred sizes including padding
        _preferredMainSize = (IsHorizontal ? maxMain : totalCross) + (IsHorizontal ? padding.horizontal : padding.vertical);
        _preferredCrossSize = (IsHorizontal ? totalCross : maxMain) + (IsHorizontal ? padding.vertical : padding.horizontal);
    }

    private void SetChildrenAlongAxis(int axis)
    {
        float containerMainSize = rectTransform.rect.size[IsHorizontal ? 0 : 1];
        float containerCrossSize = rectTransform.rect.size[IsHorizontal ? 1 : 0];

        float spacingMain = IsHorizontal ? spacingX : spacingY;
        float spacingCross = IsHorizontal ? spacingY : spacingX;

        float mainAvailable = containerMainSize - (IsHorizontal ? padding.horizontal : padding.vertical);
        float crossAvailable = containerCrossSize - (IsHorizontal ? padding.vertical : padding.horizontal);

        // Clamp available sizes to zero minimum
        mainAvailable = Mathf.Max(0, mainAvailable);
        crossAvailable = Mathf.Max(0, crossAvailable);

        List<List<RectTransform>> lines = new List<List<RectTransform>>();
        List<float> lineMainSizes = new List<float>();
        List<float> lineCrossSizes = new List<float>();

        List<RectTransform> currentLine = new List<RectTransform>();
        float lineMain = 0f;
        float lineCross = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform child = rectChildren[i];
            float childMain = LayoutUtility.GetPreferredSize(child, IsHorizontal ? 0 : 1);
            float childCross = LayoutUtility.GetPreferredSize(child, IsHorizontal ? 1 : 0);

            float spacingToAdd = currentLine.Count > 0 ? spacingMain : 0f;

            bool needWrap = IsWrap && currentLine.Count > 0 && (lineMain + spacingToAdd + childMain > mainAvailable);

            if (needWrap)
            {
                lines.Add(currentLine);
                lineMainSizes.Add(lineMain);
                lineCrossSizes.Add(lineCross);

                currentLine = new List<RectTransform>();
                lineMain = 0f;
                lineCross = 0f;
                spacingToAdd = 0f;
            }

            lineMain += spacingToAdd + childMain;
            lineCross = Mathf.Max(lineCross, childCross);
            currentLine.Add(child);
        }

        if (currentLine.Count > 0)
        {
            lines.Add(currentLine);
            lineMainSizes.Add(lineMain);
            lineCrossSizes.Add(lineCross);
        }

        bool isReverse = flexDirection == FlexDirection.RowReverse || flexDirection == FlexDirection.ColumnReverse;
        bool isWrap = flexDirection == FlexDirection.RowWrap || flexDirection == FlexDirection.ColumnWrap;

        if (isReverse && !isWrap)
        {
            lines.Reverse();
            lineMainSizes.Reverse();
            lineCrossSizes.Reverse();
        }

        float crossPos = IsHorizontal ? padding.top : padding.left;

        for (int l = 0; l < lines.Count; l++)
        {
            List<RectTransform> currentLineChildren = lines[l];
            float totalMain = lineMainSizes[l];
            int childCount = currentLineChildren.Count;

            float offset = 0f;
            float spacingBetween = spacingMain;

            switch (justifyContent)
            {
                case JustifyContent.Center:
                    offset = (mainAvailable - totalMain) / 2f;
                    spacingBetween = spacingMain;
                    break;
                case JustifyContent.FlexEnd:
                    offset = mainAvailable - totalMain;
                    spacingBetween = spacingMain;
                    break;
                case JustifyContent.SpaceBetween:
                    {
                        int gaps = childCount - 1;
                        float totalChildMain = totalMain - spacingMain * gaps;
                        float totalSpacing = mainAvailable - totalChildMain;
                        spacingBetween = gaps > 0 ? Mathf.Max(0, totalSpacing / gaps) : 0f;
                        offset = 0f;
                    }
                    break;
                case JustifyContent.SpaceAround:
                    {
                        int gaps = childCount;
                        float totalChildMain = totalMain - spacingMain * (childCount - 1);
                        float totalSpacing = mainAvailable - totalChildMain;
                        spacingBetween = gaps > 0 ? Mathf.Max(0, totalSpacing / gaps) : 0f;
                        offset = spacingBetween / 2f;
                    }
                    break;
                case JustifyContent.FlexStart:
                default:
                    offset = 0f;
                    spacingBetween = spacingMain;
                    break;
            }

            for (int i = 0; i < childCount; i++)
            {
                RectTransform child = currentLineChildren[i];
                float childMain = LayoutUtility.GetPreferredSize(child, IsHorizontal ? 0 : 1);
                float childCross = LayoutUtility.GetPreferredSize(child, IsHorizontal ? 1 : 0);

                var layoutElement = child.GetComponent<UiElement>();
                UiElement.AlignSelf selfAlign = layoutElement != null ? layoutElement.alignSelf : UiElement.AlignSelf.Inherit;

                AlignItems finalAlign = alignItems;
                if (selfAlign != UiElement.AlignSelf.Inherit)
                {
                    finalAlign = selfAlign switch
                    {
                        UiElement.AlignSelf.FlexStart => AlignItems.FlexStart,
                        UiElement.AlignSelf.FlexEnd => AlignItems.FlexEnd,
                        UiElement.AlignSelf.Center => AlignItems.Center,
                        UiElement.AlignSelf.Stretch => AlignItems.Stretch,
                        _ => alignItems
                    };
                }

                float crossAvailableForChild = crossAvailable;
                float crossPosChild = 0f;
                float crossSizeChild = childCross;

                switch (finalAlign)
                {
                    case AlignItems.FlexStart:
                        crossPosChild = 0f;
                        break;
                    case AlignItems.FlexEnd:
                        crossPosChild = crossAvailableForChild - crossSizeChild;
                        break;
                    case AlignItems.Center:
                        crossPosChild = (crossAvailableForChild - crossSizeChild) / 2f;
                        break;
                    case AlignItems.Stretch:
                        crossSizeChild = crossAvailableForChild;
                        crossPosChild = 0f;
                        break;
                }

                float mainPos = offset + i * (childMain + spacingBetween);

                if (IsHorizontal)
                {
                    float x = padding.left + (isReverse && !isWrap ? mainAvailable - mainPos - childMain : mainPos);
                    SetChildAlongAxis(child, 0, x, childMain);
                    SetChildAlongAxis(child, 1, crossPos + crossPosChild, crossSizeChild);
                }
                else
                {
                    float y = padding.top + (isReverse && !isWrap ? mainAvailable - mainPos - childMain : mainPos);
                    SetChildAlongAxis(child, 1, y, childMain);
                    SetChildAlongAxis(child, 0, crossPos + crossPosChild, crossSizeChild);
                }
            }

            crossPos += lineCrossSizes[l] + spacingCross;
        }
    }
}
