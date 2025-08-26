using UnityEngine;
using UnityEngine.UI;

namespace GPUI
{
 
    [AddComponentMenu("Layout/UI Combi Layout Group")]
    public class UiCombiLayoutGroup : LayoutGroup
    {
        public enum FlowDirection { Horizontal, Vertical }
    
        [SerializeField] private FlowDirection flow = FlowDirection.Horizontal;
        [SerializeField] private float spacingX = 5f;
        [SerializeField] private float spacingY = 5f;
        [SerializeField] private bool forceExpandChildWidth = false;
        [SerializeField] private bool forceExpandChildHeight = false;
        [SerializeField] private TextAnchor childAlignmentAnchor = TextAnchor.UpperLeft;
    
        public FlowDirection Flow
        {
            get => flow;
            set { SetProperty(ref flow, value); }
        }
    
        public float SpacingX
        {
            get => spacingX;
            set { SetProperty(ref spacingX, value); }
        }
    
        public float SpacingY
        {
            get => spacingY;
            set { SetProperty(ref spacingY, value); }
        }
    
        public bool ForceExpandChildWidth
        {
            get => forceExpandChildWidth;
            set { SetProperty(ref forceExpandChildWidth, value); }
        }
    
        public bool ForceExpandChildHeight
        {
            get => forceExpandChildHeight;
            set { SetProperty(ref forceExpandChildHeight, value); }
        }
    
        public TextAnchor ChildAlignmentAnchor
        {
            get => childAlignmentAnchor;
            set { SetProperty(ref childAlignmentAnchor, value); }
        }
    
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalcAlongAxis(0, true);
        }
    
        public override void CalculateLayoutInputVertical()
        {
            CalcAlongAxis(1, true);
        }
    
        public override void SetLayoutHorizontal()
        {
            SetChildrenAlongAxis(0);
        }
    
        public override void SetLayoutVertical()
        {
            SetChildrenAlongAxis(1);
        }
    
        private void CalcAlongAxis(int axis, bool isVertical)
        {
            float totalMin = padding.horizontal;
            float totalPreferred = padding.horizontal;
            float totalFlexible = 0;
    
            float totalMinY = padding.vertical;
            float totalPreferredY = padding.vertical;
            float totalFlexibleY = 0;
    
            float maxX = rectTransform.rect.width;
            float maxY = rectTransform.rect.height;
    
            float lineThickness = 0f;
            float lineLength = 0f;
    
            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform child = rectChildren[i];
                if (child == null) continue;
    
                float childWidth = LayoutUtility.GetPreferredSize(child, 0);
                float childHeight = LayoutUtility.GetPreferredSize(child, 1);
    
                if (flow == FlowDirection.Horizontal)
                {
                    if (lineLength + childWidth + (lineLength > 0 ? spacingX : 0) > maxX && lineLength > 0)
                    {
                        totalMinY += lineThickness + spacingY;
                        totalPreferredY += lineThickness + spacingY;
                        lineLength = 0;
                        lineThickness = 0;
                    }
                    lineLength += (lineLength > 0 ? spacingX : 0) + childWidth;
                    lineThickness = Mathf.Max(lineThickness, childHeight);
    
                    totalPreferred = Mathf.Max(totalPreferred, lineLength + padding.horizontal);
                }
                else // Vertical flow
                {
                    if (lineLength + childHeight + (lineLength > 0 ? spacingY : 0) > maxY && lineLength > 0)
                    {
                        totalMin += lineThickness + spacingX;
                        totalPreferred += lineThickness + spacingX;
                        lineLength = 0;
                        lineThickness = 0;
                    }
                    lineLength += (lineLength > 0 ? spacingY : 0) + childHeight;
                    lineThickness = Mathf.Max(lineThickness, childWidth);
    
                    totalPreferredY = Mathf.Max(totalPreferredY, lineLength + padding.vertical);
                }
            }
    
            if (flow == FlowDirection.Horizontal)
            {
                totalMinY += lineThickness;
                totalPreferredY += lineThickness;
            }
            else
            {
                totalMin += lineThickness;
                totalPreferred += lineThickness;
            }
    
            SetLayoutInputForAxis(totalMin, totalPreferred, totalFlexible, 0);
            SetLayoutInputForAxis(totalMinY, totalPreferredY, totalFlexibleY, 1);
        }
    
        private void SetChildrenAlongAxis(int axis)
        {
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;
    
            float x = padding.left;
            float y = padding.top;
            float lineThickness = 0f;
    
            if (flow == FlowDirection.Horizontal)
            {
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    RectTransform child = rectChildren[i];
                    float childWidth = LayoutUtility.GetPreferredSize(child, 0);
                    float childHeight = LayoutUtility.GetPreferredSize(child, 1);
    
                    if (x + childWidth > parentWidth - padding.right && x > padding.left)
                    {
                        x = padding.left;
                        y += lineThickness + spacingY;
                        lineThickness = 0f;
                    }
    
                    Vector2 alignedPos = GetAlignedPosition(childWidth, childHeight, parentWidth, parentHeight, x, y, lineThickness);
                    SetChildAlongAxis(child, 0, alignedPos.x, childWidth);
                    SetChildAlongAxis(child, 1, alignedPos.y, childHeight);
    
                    x += childWidth + spacingX;
                    lineThickness = Mathf.Max(lineThickness, childHeight);
                }
            }
            else
            {
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    RectTransform child = rectChildren[i];
                    float childWidth = LayoutUtility.GetPreferredSize(child, 0);
                    float childHeight = LayoutUtility.GetPreferredSize(child, 1);
    
                    if (y + childHeight > parentHeight - padding.bottom && y > padding.top)
                    {
                        y = padding.top;
                        x += lineThickness + spacingX;
                        lineThickness = 0f;
                    }
    
                    Vector2 alignedPos = GetAlignedPosition(childWidth, childHeight, parentWidth, parentHeight, x, y, lineThickness);
                    SetChildAlongAxis(child, 0, alignedPos.x, childWidth);
                    SetChildAlongAxis(child, 1, alignedPos.y, childHeight);
    
                    y += childHeight + spacingY;
                    lineThickness = Mathf.Max(lineThickness, childWidth);
                }
            }
        }
    
        private Vector2 GetAlignedPosition(float childWidth, float childHeight, float parentWidth, float parentHeight, float x, float y, float lineThickness)
        {
            float alignedX = x;
            float alignedY = y;
    
            switch (childAlignmentAnchor)
            {
                case TextAnchor.UpperCenter:
                    alignedX = x + (parentWidth - padding.horizontal - childWidth) / 2f;
                    break;
                case TextAnchor.UpperRight:
                    alignedX = parentWidth - padding.right - childWidth;
                    break;
                case TextAnchor.MiddleLeft:
                    alignedY = y + (parentHeight - padding.vertical - childHeight) / 2f;
                    break;
                case TextAnchor.MiddleCenter:
                    alignedX = x + (parentWidth - padding.horizontal - childWidth) / 2f;
                    alignedY = y + (parentHeight - padding.vertical - childHeight) / 2f;
                    break;
                case TextAnchor.MiddleRight:
                    alignedX = parentWidth - padding.right - childWidth;
                    alignedY = y + (parentHeight - padding.vertical - childHeight) / 2f;
                    break;
                case TextAnchor.LowerLeft:
                    alignedY = parentHeight - padding.bottom - childHeight;
                    break;
                case TextAnchor.LowerCenter:
                    alignedX = x + (parentWidth - padding.horizontal - childWidth) / 2f;
                    alignedY = parentHeight - padding.bottom - childHeight;
                    break;
                case TextAnchor.LowerRight:
                    alignedX = parentWidth - padding.right - childWidth;
                    alignedY = parentHeight - padding.bottom - childHeight;
                    break;
            }
    
            return new Vector2(alignedX, alignedY);
        }
    }
    
}