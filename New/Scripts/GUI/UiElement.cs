using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using ThisOtherThing.UI.Shapes;
using static ThisOtherThing.UI.GeoUtils;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(CanvasGroup))]
public class UiElement : SerializedMonoBehaviour
{

    [HideInInspector]
    public CanvasGroup canvasGroup;

    [HideInInspector]
    public RectTransform rectTransform;

    [FoldoutGroup("UI Skin")]
    public ComponentSkinDataObject skinData;

    [FoldoutGroup("UI Elements")]
    public Graphic backgroundGraphic;

    [FoldoutGroup("Settings")]
    [InfoBox("If no animations are set, the default fade-in/fade-out are used!")]

    [FoldoutGroup("Settings")]
    [OdinSerialize, NonSerialized]
    public UiAnimationData setActiveAnimation;

    [FoldoutGroup("Settings")]
    [OdinSerialize, NonSerialized]
    public UiAnimationData setInactiveAnimation;


    public void Awake()
    {


        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        ApplySkinData();

    }

    private void OnValidate()
    {

        ApplySkinData();

    }

    public virtual void FadeElement(bool fadeIn = false)
    {

        if(fadeIn)
        {

            if(setActiveAnimation == null || setActiveAnimation.animation.GetType() == typeof(UiAnimationBase))
            {

                canvasGroup.DOFade(1f, .5f)
                    .OnStart(() =>
                    {

                        canvasGroup.blocksRaycasts = true;
                        canvasGroup.interactable = true;

                    });

            }
            else
                setActiveAnimation.Play();

        }
        else
        {

            if (setInactiveAnimation == null || setInactiveAnimation.animation.GetType() == typeof(UiAnimationBase))
            {

                canvasGroup.DOFade(0f, .5f)
                    .OnStart(() =>
                    {

                        canvasGroup.blocksRaycasts = false;
                        canvasGroup.interactable = false;

                    });

            }
            else
                setInactiveAnimation.Play();

        }

    }

    public virtual void ApplySkinData()
    {

        if (skinData == null)
            return;

        if (!TryGetComponent<UiCombiLayoutGroup>(out UiCombiLayoutGroup layoutGroup))
            layoutGroup = this.AddComponent<UiCombiLayoutGroup>();

        if(skinData.useLayoutOptions)
        {

            switch (skinData.childAlignmentAxis)
            {

                case ComponentSkinDataObject.UiElementChildAlignmentAxis.None:
                    layoutGroup.enabled = true;
                    break;
                case ComponentSkinDataObject.UiElementChildAlignmentAxis.Vertical:
                    layoutGroup.enabled = true;
                    layoutGroup.padding = skinData.layoutMargin;
                    layoutGroup.spacing = skinData.layoutSpacing;
                    layoutGroup.isVertical = true;
                    layoutGroup.childAlignment = skinData.childAlignment;
                    break;
                case ComponentSkinDataObject.UiElementChildAlignmentAxis.Horizontal:
                    layoutGroup.enabled = true;
                    layoutGroup.padding = skinData.layoutMargin;
                    layoutGroup.spacing = skinData.layoutSpacing;
                    layoutGroup.isVertical = false;
                    layoutGroup.childAlignment = skinData.childAlignment;
                    break;

            }

            if (!TryGetComponent<ContentSizeFitter>(out ContentSizeFitter contentSizeFitter))
                contentSizeFitter = this.AddComponent<ContentSizeFitter>();


            if (skinData.layoutSizingOption.HasFlag(ComponentSkinDataObject.UiElementSizingOptions.ContentSizeFitted))
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
            else if (skinData.layoutSizingOption.HasFlag(ComponentSkinDataObject.UiElementSizingOptions.ParentSizeFixed))
            {

                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

                if (skinData.layoutSizingOption.HasFlag(ComponentSkinDataObject.UiElementSizingOptions.ControlChildWidth))
                {

                    layoutGroup.childControlWidth = true;
                    layoutGroup.childForceExpandWidth = true;
                    layoutGroup.childScaleWidth = false;

                }
                if (skinData.layoutSizingOption.HasFlag(ComponentSkinDataObject.UiElementSizingOptions.ControlChildHeight))
                {

                    layoutGroup.childControlHeight = true;
                    layoutGroup.childForceExpandHeight = true;
                    layoutGroup.childScaleHeight = false;

                }

            }

        }

        if (backgroundGraphic != null)
        {

            if(backgroundGraphic is Rectangle)
            {

                (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
                (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;

                (backgroundGraphic as Rectangle).Sprite = skinData.backgroundSprite;

                (backgroundGraphic as Rectangle).ShapeProperties.DrawOutline = true;
                (backgroundGraphic as Rectangle).OutlineProperties.Type = ThisOtherThing.UI.GeoUtils.OutlineProperties.LineType.Inner;
                (backgroundGraphic as Rectangle).OutlineProperties.LineWeight = skinData.outlineWidth;

                if (skinData.useMaxRadius)
                {

                    (backgroundGraphic as Rectangle).RoundedProperties.Type = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.RoundedType.Uniform;
                    (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;
                    (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;
                    (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;
                    (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;
                    (backgroundGraphic as Rectangle).RoundedProperties.UseMaxRadius = true;

                }
                else
                {

                    (backgroundGraphic as Rectangle).RoundedProperties.Type = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.RoundedType.Individual;
                    (backgroundGraphic as Rectangle).RoundedProperties.TLRadius = skinData.backgroundRadiusTL;
                    (backgroundGraphic as Rectangle).RoundedProperties.TRRadius = skinData.backgroundRadiusTR;
                    (backgroundGraphic as Rectangle).RoundedProperties.BRRadius = skinData.backgroundRadiusBR;
                    (backgroundGraphic as Rectangle).RoundedProperties.BLRadius = skinData.backgroundRadiusBL;

                }

                (backgroundGraphic as Rectangle).RoundedProperties.ResolutionMode = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.ResolutionType.Uniform;
                (backgroundGraphic as Rectangle).RoundedProperties.UniformResolution.Resolution = RoundingProperties.ResolutionType.Fixed;
                (backgroundGraphic as Rectangle).RoundedProperties.UniformResolution.FixedResolution = 16;

                if ((backgroundGraphic as Rectangle).ShadowProperties.Shadows == null || (backgroundGraphic as Rectangle).ShadowProperties.Shadows.Length == 0)
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

}