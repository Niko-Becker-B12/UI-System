using UnityEngine;
using UnityEngine.UI;

namespace GPUI
{
    [AddComponentMenu("Layout/Ui Combi Layout Group")]
    [RequireComponent(typeof(RectTransform))]
    public class UiCombiLayoutGroup : LayoutGroup, ILayoutSelfController
    {
        public enum FlowDirection { Horizontal, Vertical }
        public enum JustifyContent { Start, Center, End, SpaceBetween, SpaceAround }

        [SerializeField] private FlowDirection flow = FlowDirection.Horizontal;
        [SerializeField] private float spacingX = 5f;
        [SerializeField] private float spacingY = 5f;
        [SerializeField] private bool forceExpandChildWidth = false;
        [SerializeField] private bool forceExpandChildHeight = false;
        [SerializeField] private bool enableWrapping = true;
        [SerializeField] private JustifyContent justifyContent = JustifyContent.Start;

        public FlowDirection Flow { get => flow; set { SetProperty(ref flow, value); } }
        public float SpacingX { get => spacingX; set { SetProperty(ref spacingX, value); } }
        public float SpacingY { get => spacingY; set { SetProperty(ref spacingY, value); } }
        public bool ForceExpandChildWidth { get => forceExpandChildWidth; set { SetProperty(ref forceExpandChildWidth, value); } }
        public bool ForceExpandChildHeight { get => forceExpandChildHeight; set { SetProperty(ref forceExpandChildHeight, value); } }
        public bool EnableWrapping { get => enableWrapping; set { SetProperty(ref enableWrapping, value); } }
        public JustifyContent Justify { get => justifyContent; set { SetProperty(ref justifyContent, value); } }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalculateSizes();
        }

        public override void CalculateLayoutInputVertical()
        {
            CalculateSizes();
        }

        public override void SetLayoutHorizontal() { SetChildrenAlongAxis(); }
        public override void SetLayoutVertical() { SetChildrenAlongAxis(); }

        private float GetChildSize(RectTransform child, int axis, bool forceExpand, float availableSpace)
        {
            var layoutElement = child.GetComponent<LayoutElement>();
            float preferred = LayoutUtility.GetPreferredSize(child, axis);
            float min = LayoutUtility.GetMinSize(child, axis);

            if (layoutElement != null)
            {
                if (axis == 0 && layoutElement.preferredWidth >= 0)
                    preferred = layoutElement.preferredWidth;
                else if (axis == 1 && layoutElement.preferredHeight >= 0)
                    preferred = layoutElement.preferredHeight;
            }

            float size = Mathf.Max(min, preferred);
            if (forceExpand)
                size = availableSpace;
            return size;
        }

        private void CalculateSizes()
        {
            float totalWidth = padding.horizontal;
            float totalHeight = padding.vertical;

            float lineWidth = 0f;
            float lineHeight = 0f;

            float parentWidth = rectTransform.rect.width > 0 ? rectTransform.rect.width : float.MaxValue;
            float parentHeight = rectTransform.rect.height > 0 ? rectTransform.rect.height : float.MaxValue;

            if (flow == FlowDirection.Horizontal)
            {
                foreach (RectTransform child in rectChildren)
                {
                    float w = GetChildSize(child, 0, forceExpandChildWidth, parentWidth - padding.horizontal);
                    float h = GetChildSize(child, 1, forceExpandChildHeight, parentHeight - padding.vertical);

                    if (enableWrapping && lineWidth + w > parentWidth - padding.horizontal && lineWidth > 0)
                    {
                        totalWidth = Mathf.Max(totalWidth, lineWidth + padding.horizontal);
                        totalHeight += lineHeight + spacingY;
                        lineWidth = 0f;
                        lineHeight = 0f;
                    }

                    lineWidth += w + spacingX;
                    lineHeight = Mathf.Max(lineHeight, h);
                }

                totalWidth = Mathf.Max(totalWidth, lineWidth + padding.horizontal);
                totalHeight += lineHeight;

                SetLayoutInputForAxis(totalWidth, totalWidth, -1, 0);
                SetLayoutInputForAxis(totalHeight, totalHeight, -1, 1);
            }
            else
            {
                foreach (RectTransform child in rectChildren)
                {
                    float w = GetChildSize(child, 0, forceExpandChildWidth, parentWidth - padding.horizontal);
                    float h = GetChildSize(child, 1, forceExpandChildHeight, parentHeight - padding.vertical);

                    if (enableWrapping && lineHeight + h > parentHeight - padding.vertical && lineHeight > 0)
                    {
                        totalHeight = Mathf.Max(totalHeight, lineHeight + padding.vertical);
                        totalWidth += lineWidth + spacingX;
                        lineWidth = 0f;
                        lineHeight = 0f;
                    }

                    lineHeight += h + spacingY;
                    lineWidth = Mathf.Max(lineWidth, w);
                }

                totalHeight = Mathf.Max(totalHeight, lineHeight + padding.vertical);
                totalWidth += lineWidth;

                SetLayoutInputForAxis(totalWidth, totalWidth, -1, 0);
                SetLayoutInputForAxis(totalHeight, totalHeight, -1, 1);
            }
        }

        private void SetChildrenAlongAxis()
        {
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;
            float x = padding.left;
            float y = padding.top;
            float lineThickness = 0f;

            int count = rectChildren.Count;

            if (flow == FlowDirection.Horizontal)
            {
                float totalWidth = 0f;
                foreach (var child in rectChildren) totalWidth += GetChildSize(child, 0, false, parentWidth);
                float availableSpace = Mathf.Max(0, parentWidth - padding.horizontal - totalWidth - spacingX * (count - 1));
                float offset = 0f;
                float spacing = spacingX;

                switch (justifyContent)
                {
                    case JustifyContent.Center: offset = availableSpace / 2f; break;
                    case JustifyContent.End: offset = availableSpace; break;
                    case JustifyContent.SpaceBetween: spacing = count > 1 ? spacingX + availableSpace / (count - 1) : spacingX; break;
                    case JustifyContent.SpaceAround: spacing = spacingX + availableSpace / count; offset = spacing / 2f; break;
                }

                x += offset;

                for (int i = 0; i < count; i++)
                {
                    RectTransform child = rectChildren[i];
                    float width = GetChildSize(child, 0, forceExpandChildWidth, parentWidth - padding.horizontal);
                    float height = GetChildSize(child, 1, forceExpandChildHeight, parentHeight - padding.vertical);

                    if (enableWrapping && x + width > parentWidth - padding.right && x > padding.left)
                    {
                        x = padding.left + offset;
                        y += lineThickness + spacingY;
                        lineThickness = 0f;
                    }

                    AlignChild(child, ref x, ref y, width, height, parentWidth, parentHeight);
                    x += width + spacing;
                    lineThickness = Mathf.Max(lineThickness, height);
                }
            }
            else
            {
                float totalHeight = 0f;
                foreach (var child in rectChildren) totalHeight += GetChildSize(child, 1, false, parentHeight);
                float availableSpace = Mathf.Max(0, parentHeight - padding.vertical - totalHeight - spacingY * (count - 1));
                float offset = 0f;
                float spacing = spacingY;

                switch (justifyContent)
                {
                    case JustifyContent.Center: offset = availableSpace / 2f; break;
                    case JustifyContent.End: offset = availableSpace; break;
                    case JustifyContent.SpaceBetween: spacing = count > 1 ? spacingY + availableSpace / (count - 1) : spacingY; break;
                    case JustifyContent.SpaceAround: spacing = spacingY + availableSpace / count; offset = spacing / 2f; break;
                }

                y += offset;

                for (int i = 0; i < count; i++)
                {
                    RectTransform child = rectChildren[i];
                    float width = GetChildSize(child, 0, forceExpandChildWidth, parentWidth - padding.horizontal);
                    float height = GetChildSize(child, 1, forceExpandChildHeight, parentHeight - padding.vertical);

                    if (enableWrapping && y + height > parentHeight - padding.bottom && y > padding.top)
                    {
                        y = padding.top + offset;
                        x += lineThickness + spacingX;
                        lineThickness = 0f;
                    }

                    AlignChild(child, ref x, ref y, width, height, parentWidth, parentHeight);
                    y += height + spacing;
                    lineThickness = Mathf.Max(lineThickness, width);
                }
            }
        }

        private void AlignChild(RectTransform child, ref float x, ref float y, float width, float height, float parentWidth, float parentHeight)
        {
            float posX = x;
            float posY = y;

            if (childAlignment == TextAnchor.UpperCenter || childAlignment == TextAnchor.MiddleCenter || childAlignment == TextAnchor.LowerCenter)
                posX += (parentWidth - padding.horizontal - width) / 2f;
            else if (childAlignment == TextAnchor.UpperRight || childAlignment == TextAnchor.MiddleRight || childAlignment == TextAnchor.LowerRight)
                posX += parentWidth - padding.horizontal - width;

            if (childAlignment == TextAnchor.MiddleLeft || childAlignment == TextAnchor.MiddleCenter || childAlignment == TextAnchor.MiddleRight)
                posY += (parentHeight - padding.vertical - height) / 2f;
            else if (childAlignment == TextAnchor.LowerLeft || childAlignment == TextAnchor.LowerCenter || childAlignment == TextAnchor.LowerRight)
                posY += parentHeight - padding.vertical - height;

            posX = Mathf.Clamp(posX, padding.left, parentWidth - padding.right - width);
            posY = Mathf.Clamp(posY, padding.top, parentHeight - padding.bottom - height);

            SetChildAlongAxis(child, 0, posX, width);
            SetChildAlongAxis(child, 1, posY, height);
        }
    }
}
