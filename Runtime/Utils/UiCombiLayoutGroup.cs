using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GPUI
{
    public enum FlexDirection
    {
        Row,
        RowReverse,
        Column,
        ColumnReverse
    }

    public enum FlexWrap
    {
        NoWrap,
        Wrap,
        WrapReverse
    }

    public enum JustifyContent
    {
        FlexStart,
        FlexEnd,
        Center,
        SpaceBetween,
        SpaceAround,
        SpaceEvenly
    }

    public enum AlignItems
    {
        FlexStart,
        FlexEnd,
        Center,
        Stretch
    }

    public enum AlignContent
    {
        FlexStart,
        FlexEnd,
        Center,
        SpaceBetween,
        SpaceAround,
        Stretch
    }

    [System.Serializable]
    public class FlexItem
    {
        public RectTransform rectTransform;
        public float flexGrow = 0f;
        public float flexShrink = 1f;
        public float flexBasis = -1f; // -1 means auto
        public AlignItems alignSelf = AlignItems.Stretch;
        
        [System.NonSerialized]
        public Vector2 preferredSize;
        [System.NonSerialized]
        public Vector2 minSize;
        [System.NonSerialized]
        public Vector2 maxSize = Vector2.positiveInfinity;
    }

    [AddComponentMenu("Layout/Combi Layout Group")]
    public class UiCombiLayoutGroup : LayoutGroup
    {
        [Header("Flex Container Properties")]
        [SerializeField] private FlexDirection m_FlexDirection = FlexDirection.Row;
        [SerializeField] private FlexWrap m_FlexWrap = FlexWrap.NoWrap;
        [SerializeField] private JustifyContent m_JustifyContent = JustifyContent.FlexStart;
        [SerializeField] private AlignItems m_AlignItems = AlignItems.Stretch;
        [SerializeField] private AlignContent m_AlignContent = AlignContent.Stretch;
        
        [Header("Spacing")]
        [SerializeField] private float m_Gap = 0f;
        [SerializeField] private float m_RowGap = -1f; // -1 means use gap
        [SerializeField] private float m_ColumnGap = -1f; // -1 means use gap

        private List<FlexItem> flexItems = new List<FlexItem>();
        private List<List<FlexItem>> flexLines = new List<List<FlexItem>>();

        // Properties
        public FlexDirection flexDirection 
        { 
            get => m_FlexDirection; 
            set { m_FlexDirection = value; SetDirty(); } 
        }
        
        public FlexWrap flexWrap 
        { 
            get => m_FlexWrap; 
            set { m_FlexWrap = value; SetDirty(); } 
        }
        
        public JustifyContent justifyContent 
        { 
            get => m_JustifyContent; 
            set { m_JustifyContent = value; SetDirty(); } 
        }
        
        public AlignItems alignItems 
        { 
            get => m_AlignItems; 
            set { m_AlignItems = value; SetDirty(); } 
        }
        
        public AlignContent alignContent 
        { 
            get => m_AlignContent; 
            set { m_AlignContent = value; SetDirty(); } 
        }
        
        public float gap 
        { 
            get => m_Gap; 
            set { m_Gap = value; SetDirty(); } 
        }
        
        public float rowGap 
        { 
            get => m_RowGap < 0 ? m_Gap : m_RowGap; 
            set { m_RowGap = value; SetDirty(); } 
        }
        
        public float columnGap 
        { 
            get => m_ColumnGap < 0 ? m_Gap : m_ColumnGap; 
            set { m_ColumnGap = value; SetDirty(); } 
        }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CollectFlexItems();
            CalculatePreferredSizes();
        }

        public override void CalculateLayoutInputVertical()
        {
            // Vertical calculations are handled in SetLayoutHorizontal/Vertical
        }

        public override void SetLayoutHorizontal()
        {
            PerformFlexLayout();
        }

        public override void SetLayoutVertical()
        {
            // Layout is fully handled in SetLayoutHorizontal for flex
        }

        private void CollectFlexItems()
        {
            flexItems.Clear();
            
            for (int i = 0; i < rectChildren.Count; i++)
            {
                var child = rectChildren[i];
                var flexItemComponent = child.GetComponent<FlexItemComponent>();
                
                var flexItem = new FlexItem
                {
                    rectTransform = child
                };
                
                if (flexItemComponent != null)
                {
                    flexItem.flexGrow = flexItemComponent.flexGrow;
                    flexItem.flexShrink = flexItemComponent.flexShrink;
                    flexItem.flexBasis = flexItemComponent.flexBasis;
                    flexItem.alignSelf = flexItemComponent.alignSelf;
                }
                
                flexItems.Add(flexItem);
            }
        }

        private void CalculatePreferredSizes()
        {
            foreach (var item in flexItems)
            {
                var layoutElement = item.rectTransform.GetComponent<LayoutElement>();
                
                // Get preferred size from LayoutElement or content
                float prefWidth = layoutElement?.preferredWidth ?? -1f;
                float prefHeight = layoutElement?.preferredHeight ?? -1f;
                
                if (prefWidth < 0) prefWidth = LayoutUtility.GetPreferredWidth(item.rectTransform);
                if (prefHeight < 0) prefHeight = LayoutUtility.GetPreferredHeight(item.rectTransform);
                
                item.preferredSize = new Vector2(prefWidth, prefHeight);
                
                // Min/Max sizes
                item.minSize = new Vector2(
                    layoutElement?.minWidth ?? 0f,
                    layoutElement?.minHeight ?? 0f
                );
                
                item.maxSize = new Vector2(
                    layoutElement?.preferredWidth ?? float.MaxValue,
                    layoutElement?.preferredHeight ?? float.MaxValue
                );
            }
        }

        private void PerformFlexLayout()
        {
            if (flexItems.Count == 0) return;

            bool isRow = IsRowDirection();
            Vector2 containerSize = rectTransform.rect.size;
            Vector2 availableSize = containerSize - new Vector2(padding.horizontal, padding.vertical);
            
            // Create flex lines (handle wrapping)
            CreateFlexLines(availableSize);
            
            // Layout each flex line
            float crossAxisOffset = padding.top;
            
            for (int lineIndex = 0; lineIndex < flexLines.Count; lineIndex++)
            {
                var line = flexLines[lineIndex];
                float lineHeight = LayoutFlexLine(line, availableSize, crossAxisOffset, isRow);
                
                if (isRow)
                    crossAxisOffset += lineHeight + (lineIndex < flexLines.Count - 1 ? rowGap : 0);
                else
                    crossAxisOffset += lineHeight + (lineIndex < flexLines.Count - 1 ? columnGap : 0);
            }
        }

        private void CreateFlexLines(Vector2 availableSize)
        {
            flexLines.Clear();
            
            if (m_FlexWrap == FlexWrap.NoWrap)
            {
                flexLines.Add(new List<FlexItem>(flexItems));
                return;
            }
            
            bool isRow = IsRowDirection();
            float maxMainSize = isRow ? availableSize.x : availableSize.y;
            float currentLineSize = 0f;
            var currentLine = new List<FlexItem>();
            
            foreach (var item in flexItems)
            {
                float itemMainSize = GetFlexBasisOrPreferredSize(item, isRow);
                float gap = isRow ? columnGap : rowGap;
                
                if (currentLine.Count > 0 && currentLineSize + gap + itemMainSize > maxMainSize)
                {
                    // Start new line
                    flexLines.Add(currentLine);
                    currentLine = new List<FlexItem>();
                    currentLineSize = 0f;
                }
                
                currentLine.Add(item);
                currentLineSize += itemMainSize + (currentLine.Count > 1 ? gap : 0);
            }
            
            if (currentLine.Count > 0)
            {
                flexLines.Add(currentLine);
            }
            
            if (m_FlexWrap == FlexWrap.WrapReverse)
            {
                flexLines.Reverse();
            }
        }

        private float LayoutFlexLine(List<FlexItem> line, Vector2 availableSize, float crossAxisOffset, bool isRow)
        {
            if (line.Count == 0) return 0f;
            
            // Calculate main axis layout
            float mainAxisSize = isRow ? availableSize.x : availableSize.y;
            float crossAxisSize = isRow ? availableSize.y : availableSize.x;
            
            // Calculate total flex basis and available space
            float totalFlexBasis = 0f;
            float totalGaps = (line.Count - 1) * (isRow ? columnGap : rowGap);
            
            foreach (var item in line)
            {
                totalFlexBasis += GetFlexBasisOrPreferredSize(item, isRow);
            }
            
            float freeSpace = mainAxisSize - totalFlexBasis - totalGaps;
            
            // Distribute free space using flex grow/shrink
            var itemMainSizes = new float[line.Count];
            DistributeFreeSpace(line, itemMainSizes, freeSpace, isRow);
            
            // Position items along main axis
            float mainAxisOffset = padding.left;
            if (!isRow) mainAxisOffset = padding.top;
            
            mainAxisOffset += GetJustifyContentOffset(freeSpace, line.Count);
            float justifySpacing = GetJustifyContentSpacing(freeSpace, line.Count);
            
            float maxCrossSize = 0f;
            
            for (int i = 0; i < line.Count; i++)
            {
                var item = line[i];
                float itemMainSize = itemMainSizes[i];
                float itemCrossSize = GetItemCrossSize(item, itemMainSize, crossAxisSize, isRow);
                
                maxCrossSize = Mathf.Max(maxCrossSize, itemCrossSize);
                
                // Position the item
                Vector2 position = Vector2.zero;
                Vector2 size = Vector2.zero;
                
                if (isRow)
                {
                    position.x = mainAxisOffset;
                    position.y = GetAlignItemsOffset(item, itemCrossSize, crossAxisSize, crossAxisOffset);
                    size.x = itemMainSize;
                    size.y = itemCrossSize;
                }
                else
                {
                    position.x = GetAlignItemsOffset(item, itemCrossSize, crossAxisSize, crossAxisOffset);
                    position.y = mainAxisOffset;
                    size.x = itemCrossSize;
                    size.y = itemMainSize;
                }
                
                SetChildAlongAxis(item.rectTransform, 0, position.x, size.x);
                SetChildAlongAxis(item.rectTransform, 1, position.y, size.y);
                
                mainAxisOffset += itemMainSize + (isRow ? columnGap : rowGap) + justifySpacing;
            }
            
            return maxCrossSize;
        }

        private void DistributeFreeSpace(List<FlexItem> line, float[] itemMainSizes, float freeSpace, bool isRow)
        {
            // Initialize with flex basis
            for (int i = 0; i < line.Count; i++)
            {
                itemMainSizes[i] = GetFlexBasisOrPreferredSize(line[i], isRow);
            }
            
            if (freeSpace > 0)
            {
                // Distribute positive free space using flex-grow
                float totalFlexGrow = 0f;
                foreach (var item in line)
                {
                    totalFlexGrow += item.flexGrow;
                }
                
                if (totalFlexGrow > 0)
                {
                    for (int i = 0; i < line.Count; i++)
                    {
                        float growAmount = (line[i].flexGrow / totalFlexGrow) * freeSpace;
                        itemMainSizes[i] += growAmount;
                    }
                }
            }
            else if (freeSpace < 0)
            {
                // Distribute negative free space using flex-shrink
                float totalFlexShrink = 0f;
                foreach (var item in line)
                {
                    totalFlexShrink += item.flexShrink * GetFlexBasisOrPreferredSize(item, isRow);
                }
                
                if (totalFlexShrink > 0)
                {
                    for (int i = 0; i < line.Count; i++)
                    {
                        float shrinkFactor = (line[i].flexShrink * GetFlexBasisOrPreferredSize(line[i], isRow)) / totalFlexShrink;
                        float shrinkAmount = shrinkFactor * Mathf.Abs(freeSpace);
                        itemMainSizes[i] = Mathf.Max(0, itemMainSizes[i] - shrinkAmount);
                    }
                }
            }
        }

        private float GetFlexBasisOrPreferredSize(FlexItem item, bool isRow)
        {
            if (item.flexBasis >= 0)
                return item.flexBasis;
            
            return isRow ? item.preferredSize.x : item.preferredSize.y;
        }

        private float GetItemCrossSize(FlexItem item, float itemMainSize, float availableCrossSize, bool isRow)
        {
            AlignItems align = item.alignSelf != AlignItems.Stretch ? item.alignSelf : m_AlignItems;
            
            if (align == AlignItems.Stretch)
            {
                return availableCrossSize;
            }
            
            return isRow ? item.preferredSize.y : item.preferredSize.x;
        }

        private float GetJustifyContentOffset(float freeSpace, int itemCount)
        {
            switch (m_JustifyContent)
            {
                case JustifyContent.FlexEnd:
                    return freeSpace;
                case JustifyContent.Center:
                    return freeSpace * 0.5f;
                case JustifyContent.SpaceBetween:
                case JustifyContent.SpaceAround:
                case JustifyContent.SpaceEvenly:
                    return GetJustifyContentSpacing(freeSpace, itemCount) * 0.5f;
                default:
                    return 0f;
            }
        }

        private float GetJustifyContentSpacing(float freeSpace, int itemCount)
        {
            if (itemCount <= 1) return 0f;
            
            switch (m_JustifyContent)
            {
                case JustifyContent.SpaceBetween:
                    return freeSpace / (itemCount - 1);
                case JustifyContent.SpaceAround:
                    return freeSpace / itemCount;
                case JustifyContent.SpaceEvenly:
                    return freeSpace / (itemCount + 1);
                default:
                    return 0f;
            }
        }

        private float GetAlignItemsOffset(FlexItem item, float itemCrossSize, float availableCrossSize, float crossAxisOffset)
        {
            AlignItems align = item.alignSelf != AlignItems.Stretch ? item.alignSelf : m_AlignItems;
            float offset = crossAxisOffset;
            
            switch (align)
            {
                case AlignItems.FlexEnd:
                    offset += availableCrossSize - itemCrossSize;
                    break;
                case AlignItems.Center:
                    offset += (availableCrossSize - itemCrossSize) * 0.5f;
                    break;
            }
            
            return offset;
        }

        private bool IsRowDirection()
        {
            return m_FlexDirection == FlexDirection.Row || m_FlexDirection == FlexDirection.RowReverse;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            SetDirty();
        }
    }
}