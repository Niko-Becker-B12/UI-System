using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GPUI
{
    [RequireComponent(typeof(ButtonBehavior))]

    public class UiButton : UiElementExtended
    {

        [HideInInspector] public ButtonBehavior _buttonBehavior;

        [Space] [TabGroup("Events", "OnClick")]
        public List<Function> onClickFunctions = new List<Function>();

        [Space] [TabGroup("Events", "OnEnter")]
        public List<Function> onEnterFunctions = new List<Function>();

        [Space] [TabGroup("Events", "OnExit")] public List<Function> onExitFunctions = new List<Function>();

        [Space] [TabGroup("Events", "OnReset")]
        public List<Function> onResetFunctions = new List<Function>();


        private void Start()
        {


            _buttonBehavior = GetComponent<ButtonBehavior>();

            if (backgroundGraphic != null)
                backgroundGraphic.raycastPadding = new Vector4(-8f, -8f, -8f, -8f);


            Function onClick = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            onClick.functionName.AddListener(() => { OnClick(); });

            onClickFunctions.Add(onClick);

            Function onEnter = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            onEnter.functionName.AddListener(() => { OnEnter(); });

            onEnterFunctions.Add(onEnter);

            Function onExit = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            onExit.functionName.AddListener(() => { OnExit(); });

            onExitFunctions.Add(onExit);

            Function onReset = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            onReset.functionName.AddListener(() => { OnReset(); });

            onResetFunctions.Add(onReset);


            _buttonBehavior.onMouseDown.AddRange(onClickFunctions);
            _buttonBehavior.onMouseEnter.AddRange(onEnterFunctions);
            _buttonBehavior.onMouseExit.AddRange(onExitFunctions);
            _buttonBehavior.onButtonReset.AddRange(onResetFunctions);

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

        public virtual void OnClick()
        {

            if (skinData == null)
                return;

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

        public virtual void OnEnter()
        {

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

        public virtual void OnExit()
        {

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