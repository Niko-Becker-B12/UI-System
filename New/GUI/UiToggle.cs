using Showroom.UI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleBehavior))]
public class UiToggle : UiElement
{

    public Graphic detailGraphic;

    public ToggleBehavior _toggleBehavior;

    [Space]
    [BoxGroup("On Set Active Event")]
    public List<Function> onSetActiveFunctions = new List<Function>();
    [Space]
    [BoxGroup("On Set Deactive Event")]
    public List<Function> onSetDeactiveFunctions = new List<Function>();

    [Space]
    [BoxGroup("On Enter Event")]
    public List<Function> onEnterFunctions = new List<Function>();

    [Space]
    [BoxGroup("On Exit Event")]
    public List<Function> onExitFunctions = new List<Function>();

    [Space]
    [BoxGroup("On Reset Event")]
    public List<Function> onResetFunctions = new List<Function>();


    public bool isToggleButton = false;


    public void Start()
    {

        _toggleBehavior = GetComponent<ToggleBehavior>();

        if (backgroundGraphic != null)
            backgroundGraphic.raycastPadding = new Vector4(-8f, -8f, -8f, -8f);

        _toggleBehavior.onSetActive.Clear();
        _toggleBehavior.onSetDeactive.Clear();
        _toggleBehavior.onMouseEnter.Clear();
        _toggleBehavior.onMouseExit.Clear();
        _toggleBehavior.onButtonReset.Clear();


        Function onSetActive = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onSetActive.functionName.AddListener(() =>
        {
            OnClick(true);
        });

        onSetActiveFunctions.Add(onSetActive);

        Function onSetDeactive = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onSetDeactive.functionName.AddListener(() =>
        {
            OnClick(false);
        });

        onSetDeactiveFunctions.Add(onSetDeactive);

        Function onEnter = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onEnter.functionName.AddListener(() =>
        {
            OnEnter();
        });

        onEnterFunctions.Add(onEnter);

        Function onExit = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onExit.functionName.AddListener(() =>
        {
            OnExit();
        });

        onExitFunctions.Add(onExit);

        Function onReset = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onReset.functionName.AddListener(() =>
        {
            OnReset();
        });

        onResetFunctions.Add(onReset);


        _toggleBehavior.onSetActive.AddRange(onSetActiveFunctions);
        _toggleBehavior.onSetDeactive.AddRange(onSetDeactiveFunctions);
        _toggleBehavior.onMouseEnter.AddRange(onEnterFunctions);
        _toggleBehavior.onMouseExit.AddRange(onExitFunctions);
        _toggleBehavior.onButtonReset.AddRange(onResetFunctions);

    }

    public override void ApplySkinData()
    {

        base.ApplySkinData();

        if (skinData == null)
            return;

        if (detailGraphic != null)
        {

            detailGraphic.color = skinData.detailColor.normalColor;

        }

    }

    public virtual void OnClick(bool isActive)
    {

        if(isActive)
        {

            if (backgroundGraphic != null)
            {

                if (skinData != null)
                {
                    if (skinData is UiToggleSkinDataObject)
                    {

                        (backgroundGraphic as Rectangle).ShapeProperties.FillColor = (skinData as UiToggleSkinDataObject).pressedBackgroundColor.pressedColor;
                        (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = (skinData as UiToggleSkinDataObject).pressedBackgroundColor.pressedColor;

                    }
                    else
                    {

                        (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.pressedColor;
                        (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.pressedColor;

                    }

                }

            }

            if (detailGraphic != null)
            {

                if (isToggleButton)
                    detailGraphic.gameObject.SetActive(true);

                if (skinData != null)
                    if (skinData is UiToggleSkinDataObject)
                        detailGraphic.color = (skinData as UiToggleSkinDataObject).pressedDetailColor.pressedColor;
                    else
                        detailGraphic.color = skinData.detailColor.pressedColor;

            }

            if (!_toggleBehavior.isActive)
            {

                _toggleBehavior.isActive = false;
                _toggleBehavior.OnMouseDown();

            }

        }
        else
        {

            if (backgroundGraphic != null)
            {

                if (skinData != null)
                {
                    if (skinData is UiToggleSkinDataObject)
                    {

                        (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.pressedColor;
                        (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.pressedColor;

                    }
                    else
                    {

                        (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.pressedColor;
                        (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.pressedColor;

                    }

                }

            }

            if (detailGraphic != null)
            {

                if (isToggleButton)
                    detailGraphic.gameObject.SetActive(false);

                if (skinData != null)
                    if(skinData is UiToggleSkinDataObject)
                        detailGraphic.color = (skinData as UiToggleSkinDataObject).detailColor.pressedColor;
                    else
                        detailGraphic.color = skinData.detailColor.pressedColor;

            }

            if (_toggleBehavior.isActive)
            {

                _toggleBehavior.isActive = true;
                _toggleBehavior.OnMouseDown();

            }

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
            if(_toggleBehavior.isActive)
            {
                if(skinData is UiToggleSkinDataObject)
                {
                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = (skinData as UiToggleSkinDataObject).pressedBackgroundColor.highlightedColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = (skinData as UiToggleSkinDataObject).pressedBackgroundColor.highlightedColor;
                }
                else
                {
                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.highlightedColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.highlightedColor;
                }
            }
            else
            {
                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.highlightedColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.highlightedColor;
            }

        }

        if (detailGraphic != null)
        {
            if (_toggleBehavior.isActive)
                if (skinData is UiToggleSkinDataObject)
                    detailGraphic.color = (skinData as UiToggleSkinDataObject).pressedDetailColor.highlightedColor;
                else
                    detailGraphic.color = skinData.detailColor.highlightedColor;

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
            if (_toggleBehavior.isActive)
            {
                if (skinData is UiToggleSkinDataObject)
                {
                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = (skinData as UiToggleSkinDataObject).pressedBackgroundColor.normalColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = (skinData as UiToggleSkinDataObject).pressedBackgroundColor.normalColor;
                }
                else
                {
                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;
                }
            }
            else
            {
                (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
                (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;
            }

        }

        if (detailGraphic != null)
        {
            if (_toggleBehavior.isActive)
                if (skinData is UiToggleSkinDataObject)
                    detailGraphic.color = (skinData as UiToggleSkinDataObject).pressedDetailColor.normalColor;
                else
                    detailGraphic.color = skinData.detailColor.normalColor;

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
            if (_toggleBehavior.isActive)
            {
                if (skinData is UiToggleSkinDataObject)
                {
                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = (skinData as UiToggleSkinDataObject).pressedBackgroundColor.normalColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = (skinData as UiToggleSkinDataObject).pressedBackgroundColor.normalColor;
                }
                else
                {
                    (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
                    (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;
                }
            }
            else
            {
                (backgroundGraphic as Rectangle).ShapeProperties.FillColor = skinData.backgroundColor.normalColor;
                (backgroundGraphic as Rectangle).ShapeProperties.OutlineColor = skinData.outlineColor.normalColor;
            }

        }

        if (detailGraphic != null)
        {

            if (_toggleBehavior.isActive)
            {
                if (skinData is UiToggleSkinDataObject)
                {
                    detailGraphic.color = (skinData as UiToggleSkinDataObject).pressedDetailColor.normalColor;
                }
                else
                {
                    detailGraphic.color = skinData.detailColor.normalColor;
                }
            }
            else
            {
                detailGraphic.color = skinData.detailColor.normalColor;
            }

        }

        if (backgroundGraphic != null)
            backgroundGraphic.SetAllDirty();

        if (detailGraphic != null)
            detailGraphic.SetAllDirty();

    }

}