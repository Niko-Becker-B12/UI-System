using UnityEngine;
using UnityEngine.UI;

namespace GPUI
{
    [AddComponentMenu("Layout/Flex Item")]
    [RequireComponent(typeof(RectTransform))]
    public class FlexItemComponent : MonoBehaviour, ILayoutElement
    {
        [Header("Flex Item Properties")]
        [SerializeField, Min(0)] private float m_FlexGrow = 0f;
        [SerializeField, Min(0)] private float m_FlexShrink = 1f;
        [SerializeField] private float m_FlexBasis = -1f; // -1 means auto
        [SerializeField] private AlignItems m_AlignSelf = AlignItems.Stretch;
        
        [Header("Size Constraints")]
        [SerializeField] private float m_MinWidth = -1f;
        [SerializeField] private float m_MinHeight = -1f;
        [SerializeField] private float m_PreferredWidth = -1f;
        [SerializeField] private float m_PreferredHeight = -1f;
        [SerializeField] private float m_FlexibleWidth = -1f;
        [SerializeField] private float m_FlexibleHeight = -1f;
        
        [SerializeField] private int m_LayoutPriority = 1;

        // Properties
        public float flexGrow 
        { 
            get => m_FlexGrow; 
            set { m_FlexGrow = Mathf.Max(0, value); SetDirty(); } 
        }
        
        public float flexShrink 
        { 
            get => m_FlexShrink; 
            set { m_FlexShrink = Mathf.Max(0, value); SetDirty(); } 
        }
        
        public float flexBasis 
        { 
            get => m_FlexBasis; 
            set { m_FlexBasis = value; SetDirty(); } 
        }
        
        public AlignItems alignSelf 
        { 
            get => m_AlignSelf; 
            set { m_AlignSelf = value; SetDirty(); } 
        }

        // ILayoutElement implementation
        public virtual float minWidth => m_MinWidth;
        public virtual float preferredWidth => m_PreferredWidth;
        public virtual float flexibleWidth => m_FlexibleWidth;
        public virtual float minHeight => m_MinHeight;
        public virtual float preferredHeight => m_PreferredHeight;
        public virtual float flexibleHeight => m_FlexibleHeight;
        public virtual int layoutPriority => m_LayoutPriority;

        // Convenience methods for setting flex shorthand
        public void SetFlex(float grow, float shrink = 1f, float basis = -1f)
        {
            m_FlexGrow = Mathf.Max(0, grow);
            m_FlexShrink = Mathf.Max(0, shrink);
            m_FlexBasis = basis;
            SetDirty();
        }

        public void SetFlex(float grow)
        {
            SetFlex(grow, 1f, 0f);
        }

        // Called when values change in the inspector
        protected virtual void OnValidate()
        {
            m_FlexGrow = Mathf.Max(0, m_FlexGrow);
            m_FlexShrink = Mathf.Max(0, m_FlexShrink);
            SetDirty();
        }

        protected virtual void OnEnable()
        {
            SetDirty();
        }

        protected virtual void OnDisable()
        {
            SetDirty();
        }

        protected virtual void OnDidApplyAnimationProperties()
        {
            SetDirty();
        }

        protected virtual void OnBeforeTransformParentChanged()
        {
            SetDirty();
        }

        protected virtual void OnTransformParentChanged()
        {
            SetDirty();
        }

        public virtual void CalculateLayoutInputHorizontal()
        {
            // Implementation can be added if needed
        }

        public virtual void CalculateLayoutInputVertical()
        {
            // Implementation can be added if needed
        }

        protected void SetDirty()
        {
            if (!IsActive())
                return;

            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }

        protected bool IsActive()
        {
            return isActiveAndEnabled;
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            // Set reasonable defaults
            m_FlexGrow = 0f;
            m_FlexShrink = 1f;
            m_FlexBasis = -1f;
            m_AlignSelf = AlignItems.Stretch;
        }
#endif
    }
}