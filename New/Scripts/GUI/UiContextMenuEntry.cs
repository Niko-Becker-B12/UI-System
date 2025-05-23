using Sirenix.OdinInspector;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class UiContextMenuEntry : UiButton
{

    [FoldoutGroup("UI Elements")]
    public Graphic secondaryDetailGraphic;

    public LocalizedString title;


    public override void ApplySkinData()
    {

        base.ApplySkinData();

        if (secondaryDetailGraphic != null)
        {

            secondaryDetailGraphic.color = skinData.detailColor.normalColor;

        }

        if (secondaryDetailGraphic != null)
            secondaryDetailGraphic.SetAllDirty();

    }

    public override void OnClick()
    {
        
        base.OnClick();

        if (secondaryDetailGraphic != null)
        {

            secondaryDetailGraphic.color = skinData.detailColor.pressedColor;

        }

        if (secondaryDetailGraphic != null)
            secondaryDetailGraphic.SetAllDirty();

    }

    public override void OnEnter()
    {

        base.OnEnter();

        if (secondaryDetailGraphic != null)
        {

            secondaryDetailGraphic.color = skinData.detailColor.highlightedColor;

        }

        if (secondaryDetailGraphic != null)
            secondaryDetailGraphic.SetAllDirty();

    }

    public override void OnExit()
    {

        base.OnExit();

        if (secondaryDetailGraphic != null)
        {

            secondaryDetailGraphic.color = skinData.detailColor.normalColor;

        }

        if (secondaryDetailGraphic != null)
            secondaryDetailGraphic.SetAllDirty();

    }

    public override void OnReset()
    {
        base.OnReset();

        if (secondaryDetailGraphic != null)
        {

            secondaryDetailGraphic.color = skinData.detailColor.normalColor;

        }

        if (secondaryDetailGraphic != null)
            secondaryDetailGraphic.SetAllDirty();

    }

}