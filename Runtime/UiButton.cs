using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GPUI
{


    public class UiButton : UiElementExtended
    {

        public bool clickableOnce = false;

        [ReadOnly] public bool wasClicked = false;

        bool isResetting = false;


        protected override void Start()
        {

            if (backgroundGraphic != null)
                backgroundGraphic.raycastPadding = new Vector4(-8f, -8f, -8f, -8f);

        }

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            if (skinData == null)
                return;

            if (detailGraphic != null && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                
                detailGraphic.color = detailedSkinData.detailColor.normalColor;

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        }

        public override void OnPointerClick(PointerEventData eventData)
        {

            if (skinData == null)
                return;

            if (clickableOnce)
                if (!wasClicked)
                    wasClicked = true;

            if (wasClicked)
                return;
            
            onClick?.Invoke();

            if (backgroundGraphic != null)
            {

                if (backgroundGraphic is Rectangle)
                {

                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.pressedColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.pressedColor;

                }
                else if (backgroundGraphic is Polygon)
                {

                    (backgroundGraphic as Polygon).ShapeProperties.FillColor = skinData.backgroundColor.pressedColor;

                }

            }

            if (detailGraphic != null && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                
                detailGraphic.color = detailedSkinData.detailColor.pressedColor;

            }

            if (backgroundGraphic != null)
                backgroundGraphic.SetAllDirty();

            if (detailGraphic != null)
                detailGraphic.SetAllDirty();

        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            
            base.OnPointerEnter(eventData);

            if (skinData == null)
                return;

            if (backgroundGraphic != null)
            {

                if (backgroundGraphic is Rectangle)
                {

                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor =
                        skinData.backgroundColor.highlightedColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor =
                        skinData.outlineColor.highlightedColor;

                }
                else if (backgroundGraphic is Polygon)
                {

                    (backgroundGraphic as Polygon).ShapeProperties.FillColor =
                        skinData.backgroundColor.highlightedColor;

                }

            }

            if (detailGraphic != null && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                
                detailGraphic.color = detailedSkinData.detailColor.highlightedColor;

            }

            if (backgroundGraphic != null)
                backgroundGraphic.SetAllDirty();

            if (detailGraphic != null)
                detailGraphic.SetAllDirty();

        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            
            base.OnPointerExit(eventData);

            if (skinData == null)
                return;

            if (backgroundGraphic != null)
            {

                if (backgroundGraphic is Rectangle)
                {

                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;

                }
                else if (backgroundGraphic is Polygon)
                {

                    (backgroundGraphic as Polygon).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;

                }

            }

            if (detailGraphic != null && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                
                detailGraphic.color = detailedSkinData.detailColor.normalColor;

            }

            if (backgroundGraphic != null)
                backgroundGraphic.SetAllDirty();

            if (detailGraphic != null)
                detailGraphic.SetAllDirty();

        }

        public virtual void OnReset()
        {
            
            wasClicked = false;

            if (!this.gameObject.activeSelf)
                return;
            
            if (skinData == null)
                return;

            if (backgroundGraphic != null)
            {

                if (backgroundGraphic is Rectangle)
                {

                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;

                }
                else if (backgroundGraphic is Polygon)
                {

                    (backgroundGraphic as Polygon).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;

                }

            }

            if (detailGraphic != null && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                
                detailGraphic.color = detailedSkinData.detailColor.normalColor;

            }

            if (backgroundGraphic != null)
                backgroundGraphic.SetAllDirty();

            if (detailGraphic != null)
                detailGraphic.SetAllDirty();

        }

    }
}