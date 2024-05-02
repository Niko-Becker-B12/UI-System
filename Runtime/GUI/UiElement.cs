using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using ThisOtherThing.UI.Shapes;
using static ThisOtherThing.UI.GeoUtils;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CanvasGroup))]
public class UiElement : MonoBehaviour
{

    [HideInInspector]
    public CanvasGroup canvasGroup;

    [FoldoutGroup("UI Skin")]
    public ComponentSkinDataObject skinData;
    [FoldoutGroup("UI Skin")]
    public string skinDataIdentifier;

    [FoldoutGroup("UI Elements")]
    public Graphic backgroundGraphic;


    public void Awake()
    {

        canvasGroup = GetComponent<CanvasGroup>();

        GameManager.OnLoginFinalized += delegate { ApplySkinData(); };

    }

    private void OnValidate()
    {

        ApplySkinData();

    }

    public virtual void FadeElement(bool fadeIn = false)
    {

        if(fadeIn)
        {

            canvasGroup.DOFade(1f, .5f)
                .OnStart(() =>
                {

                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.interactable = true;

                });

        }
        else
        {

            canvasGroup.DOFade(0f, .5f)
                .OnStart(() =>
                {

                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.interactable = false;

                });

        }

    }

    public virtual void ApplySkinData()
    {

        if (skinData == null)
            return;

        if(backgroundGraphic != null)
        {

            (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
            (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;
            
            (backgroundGraphic as Rectangle).ShapeProperties.DrawOutline = true;
            (backgroundGraphic as Rectangle).OutlineProperties.Type = ThisOtherThing.UI.GeoUtils.OutlineProperties.LineType.Inner;
            (backgroundGraphic as Rectangle).OutlineProperties.LineWeight = skinData.outlineWidth;
            
            (backgroundGraphic as Rectangle).RoundedProperties.Type = ThisOtherThing.UI.ShapeUtils.RoundedRects.RoundedProperties.RoundedType.Individual;
            (backgroundGraphic as Rectangle).RoundedProperties.TLRadius = skinData.backgroundRadiusTL;
            (backgroundGraphic as Rectangle).RoundedProperties.TRRadius = skinData.backgroundRadiusTR;
            (backgroundGraphic as Rectangle).RoundedProperties.BRRadius = skinData.backgroundRadiusBR;
            (backgroundGraphic as Rectangle).RoundedProperties.BLRadius = skinData.backgroundRadiusBL;
            (backgroundGraphic as Rectangle).RoundedProperties.UniformResolution.FixedResolution = 1;

            if ((backgroundGraphic as Rectangle).ShadowProperties.Shadows.Length == 0)
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

        LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.rectTransform);

    }

}