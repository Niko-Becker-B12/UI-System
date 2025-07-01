using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThisOtherThing.UI.Shapes;
using static ThisOtherThing.UI.GeoUtils;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using LitMotion;
using LitMotion.Extensions;
using ThisOtherThing.UI.ShapeUtils;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace GPUI
{
    
    [RequireComponent(typeof(CanvasGroup))]
    public class UiElement : UIBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [HideInInspector] public CanvasGroup canvasGroup;

        [HideInInspector] public RectTransform rectTransform;

        [TabGroup("Tabs", "UI Skin", SdfIconType.FileEarmarkMedical)]
        [AssetSelector(Paths = "Assets/Content/GUI/Styles/")]
        public SimpleComponentSkinDataObject skinData;

        [TabGroup("Tabs", "UI Elements", SdfIconType.Stickies)]
        [Tooltip(
            "This should be the Background Graphic of the UI Element.<br>Example: Background of a Button, not the Icon.")]
        public Graphic backgroundGraphic;

        [TabGroup("Tabs", "Animations", SdfIconType.SkipEnd)]
        [InfoBox("If no animations are set, the default fade-in/fade-out are used!")]
        [TabGroup("Tabs", "Animations")]
        [OdinSerialize, NonSerialized]
        public UiAnimationData setActiveAnimation;

        [TabGroup("Tabs", "Animations")] [OdinSerialize, NonSerialized]
        public UiAnimationData setInactiveAnimation;
        
        [Space] [TabGroup("Events", "OnClick")]
        public UnityEvent onClick;
        
        [Space] [TabGroup("Events", "OnEnter")]
        public UnityEvent onEnter;
        
        [Space] [TabGroup("Events", "OnExit")]
        public UnityEvent onExit;
        
        
        protected virtual void Awake()
        {

            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            
            Debug.Log(UiManager.Instance);

        }

        protected override void OnValidate()
        {
            
            base.OnValidate();
            
            ApplySkinData();
            
        }

        protected virtual void Start()
        {
            
            Debug.Log(UiManager.Instance);
            
            if (UiManager.Instance != null)
            {

                //Load desired SkinData
                UiManager.Instance.OnSkinChanged.AddListener(delegate
                {

                    UiManager.Instance.currentPalette.GetSkinData(out skinData, skinData);
                    ApplySkinData();

                });

            }
            else
                ApplySkinData();
            
        }

        public virtual void FadeElement(bool fadeIn = false)
        {

            if (fadeIn)
            {

                if (setActiveAnimation == null || setActiveAnimation.animation.GetType() == typeof(UiAnimationBase))
                {

                    LMotion.Create(canvasGroup.alpha, 1f, .5f)
                        .WithOnComplete(() =>
                        {
                            canvasGroup.blocksRaycasts = true;
                            canvasGroup.interactable = true;
                        })
                        .BindToAlpha(canvasGroup);
                    
                }
                else
                    setActiveAnimation.Play();

            }
            else
            {

                if (setInactiveAnimation == null || setInactiveAnimation.animation.GetType() == typeof(UiAnimationBase))
                {

                    LMotion.Create(canvasGroup.alpha, 0f, .5f)
                        .WithOnComplete(() =>
                        {
                            canvasGroup.blocksRaycasts = false;
                            canvasGroup.interactable = false;
                        })
                        .BindToAlpha(canvasGroup);

                }
                else
                    setInactiveAnimation.Play();

            }

        }
        
        public virtual void ApplySkinData()
        {

            if (skinData == null)
                return;
            
            Debug.Log($"{this.name} is applying skin data {skinData.name}");

            if (!TryGetComponent<UiCombiLayoutGroup>(out UiCombiLayoutGroup layoutGroup))
                layoutGroup = this.gameObject.AddComponent<UiCombiLayoutGroup>();

            if (skinData.useLayoutOptions)
            {
                
                layoutGroup.reverseArrangement = !skinData.layoutOptions.reverseLayout;

                switch (skinData.layoutOptions.childAlignmentAxis)
                {

                    case SimpleComponentSkinDataObject.UiElementLayoutOptions.UiElementChildAlignmentAxis.None:
                        layoutGroup.enabled = true;
                        break;
                    case SimpleComponentSkinDataObject.UiElementLayoutOptions.UiElementChildAlignmentAxis.Vertical:
                        layoutGroup.enabled = true;
                        layoutGroup.padding = skinData.layoutOptions.layoutMargin;
                        layoutGroup.spacing = skinData.layoutOptions.layoutSpacing;
                        layoutGroup.isVertical = true;
                        layoutGroup.childAlignment = skinData.layoutOptions.childAlignment;
                        break;
                    case SimpleComponentSkinDataObject.UiElementLayoutOptions.UiElementChildAlignmentAxis.Horizontal:
                        layoutGroup.enabled = true;
                        layoutGroup.padding = skinData.layoutOptions.layoutMargin;
                        layoutGroup.spacing = skinData.layoutOptions.layoutSpacing;
                        layoutGroup.isVertical = false;
                        layoutGroup.childAlignment = skinData.layoutOptions.childAlignment;
                        break;

                }

                if (!TryGetComponent<ContentSizeFitter>(out ContentSizeFitter contentSizeFitter))
                    contentSizeFitter = this.gameObject.AddComponent<ContentSizeFitter>();


                if (skinData.layoutOptions.layoutSizingOption.HasFlag(
                        SimpleComponentSkinDataObject.UiElementLayoutOptions.UiElementSizingOptions.ContentSizeFitted))
                {
                    contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                    layoutGroup.childControlHeight = false;
                    layoutGroup.childControlWidth = false;
                    layoutGroup.childForceExpandHeight = false;
                    layoutGroup.childForceExpandWidth = false;
                    layoutGroup.childScaleHeight = true;
                    layoutGroup.childScaleWidth = true;

                }
                else if (skinData.layoutOptions.layoutSizingOption.HasFlag(SimpleComponentSkinDataObject.UiElementLayoutOptions.UiElementSizingOptions
                             .ParentSizeFixed))
                {

                    contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

                    if (skinData.layoutOptions.layoutSizingOption.HasFlag(SimpleComponentSkinDataObject.UiElementLayoutOptions.UiElementSizingOptions
                            .ControlChildWidth))
                    {

                        layoutGroup.childControlWidth = true;
                        layoutGroup.childForceExpandWidth = true;
                        layoutGroup.childScaleWidth = false;

                    }

                    if (skinData.layoutOptions.layoutSizingOption.HasFlag(SimpleComponentSkinDataObject.UiElementLayoutOptions.UiElementSizingOptions
                            .ControlChildHeight))
                    {

                        layoutGroup.childControlHeight = true;
                        layoutGroup.childForceExpandHeight = true;
                        layoutGroup.childScaleHeight = false;

                    }

                }

            }

            if (backgroundGraphic != null)
            {

                if (skinData.backgroundMaterial != null)
                    backgroundGraphic.material = skinData.backgroundMaterial;

                if (backgroundGraphic is Rectangle)
                {

                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;

                    if (skinData.backgroundSprite != null)
                        (backgroundGraphic as Rectangle).Sprite = skinData.backgroundSprite;

                    (backgroundGraphic as Rectangle).ShapeProperties.DrawOutline = true;
                    (backgroundGraphic as Rectangle).OutlineProperties.Type =
                        OutlineProperties.LineType.Inner;
                    (backgroundGraphic as Rectangle).OutlineProperties.LineWeight = skinData.outlineWidth;

                    if (skinData.useMaxRadius)
                    {

                        (backgroundGraphic as Rectangle).RoundedProperties.Type = RoundedRects.RoundedProperties.RoundedType.Uniform;
                        (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;
                        (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;
                        (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;
                        (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;
                        (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;

                    }
                    else
                    {

                        (backgroundGraphic as Rectangle).RoundedProperties.Type = RoundedRects.RoundedProperties.RoundedType.Individual;
                        (backgroundGraphic as Rectangle).RoundedProperties.TLRadius = skinData.backgroundRadiusTL;
                        (backgroundGraphic as Rectangle).RoundedProperties.TRRadius = skinData.backgroundRadiusTR;
                        (backgroundGraphic as Rectangle).RoundedProperties.BRRadius = skinData.backgroundRadiusBR;
                        (backgroundGraphic as Rectangle).RoundedProperties.BLRadius = skinData.backgroundRadiusBL;

                    }

                    (backgroundGraphic as Rectangle).RoundedProperties.ResolutionMode = RoundedRects.RoundedProperties.ResolutionType.Uniform;
                    (backgroundGraphic as Rectangle).RoundedProperties.UniformResolution.Resolution =
                        RoundingProperties.ResolutionType.Fixed;
                    (backgroundGraphic as Rectangle).RoundedProperties.UniformResolution.FixedResolution =
                        skinData.shapeRoundness * 2;

                    if ((backgroundGraphic as Rectangle).ShadowProperties.Shadows == null ||
                        (backgroundGraphic as Rectangle).ShadowProperties.Shadows.Length == 0)
                    {

                        ShadowProperties[] shadows = new ShadowProperties[1];

                        shadows[0] = new ShadowProperties
                        {
                            Softness = skinData.softness,
                            Color = skinData.shadowColor,
                            Size = skinData.size,
                            Offset = skinData.offset
                        };

                        (backgroundGraphic as Rectangle).ShadowProperties.Shadows = shadows;

                    }
                    else
                        (backgroundGraphic as Rectangle).ShadowProperties.Shadows[0] = new ShadowProperties
                        {
                            Softness = skinData.softness,
                            Color = skinData.shadowColor,
                            Size = skinData.size,
                            Offset = skinData.offset
                        };

                    backgroundGraphic.SetAllDirty();
                    (backgroundGraphic as Rectangle).ForceMeshUpdate();

                }
                else if (backgroundGraphic is Polygon)
                {

                    (backgroundGraphic as Polygon).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;


                    if ((backgroundGraphic as Polygon).ShadowProperties.Shadows.Length == 0)
                    {

                        ShadowProperties[] shadows = new ShadowProperties[1];

                        shadows[0] = new ShadowProperties
                        {
                            Softness = skinData.softness,
                            Color = skinData.shadowColor,
                            Size = skinData.size,
                            Offset = skinData.offset
                        };

                        (backgroundGraphic as Polygon).ShadowProperties.Shadows = shadows;

                    }
                    else
                        (backgroundGraphic as Polygon).ShadowProperties.Shadows[0] = new ShadowProperties
                        {
                            Softness = skinData.softness,
                            Color = skinData.shadowColor,
                            Size = skinData.size,
                            Offset = skinData.offset
                        };

                    backgroundGraphic.SetAllDirty();
                    (backgroundGraphic as Polygon).ForceMeshUpdate();

                }

                LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
                LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.rectTransform);

            }

        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {

            onClick?.Invoke();
            
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            
            UiCursor.ChangeCursor(UiCursor.CursorType.Hand);

#endif
            
            onEnter?.Invoke();
            
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            
            UiCursor.ChangeCursor(UiCursor.CursorType.Arrow);

#endif
            
            onExit?.Invoke();   
         
        }
    }
}