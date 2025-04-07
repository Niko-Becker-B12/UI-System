using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class UiTab : UiToggle
{

    public Graphic secondaryDetailGraphic;

    public LocalizedString title;

    public int tabIndex;

    public Transform contentHolderParent;

    public UiTabGroup tabGroup;


    public void Awake()
    {

        base.Awake();

    }

    public override void ApplySkinData()
    {

        base.ApplySkinData();

        if(detailGraphic.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
        {

            if (title.IsEmpty)
                text.text = $"Tab {tabIndex}";
            else
            {

                title.StringChanged += text.SetText;

            }

        }

        if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
            return;

        secondaryDetailGraphic.color = skinData.detailColor.normalColor;

    }

    public override void OnClick(bool isActive)
    {


        base.OnClick(isActive);

        if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
            return;

        if (isActive)
        {

            (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).pressedDetailColor.normalColor;

        }
        else
        {

            (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).detailColor.normalColor;

        }

    }

    public override void OnEnter()
    {

        base.OnEnter();

        if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
            return;

        if (_toggleBehavior.isActive)
        {

            (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).pressedDetailColor.highlightedColor;

        }
        else
        {

            (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).detailColor.highlightedColor;

        }

    }

    public override void OnExit()
    {

        base.OnExit();

        if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
            return;

        if (_toggleBehavior.isActive)
        {

            (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).pressedDetailColor.normalColor;

        }
        else
        {

            (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).detailColor.normalColor;

        }

    }

    public override void OnReset()
    {

        base.OnReset();

        if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
            return;

        if (_toggleBehavior.isActive)
        {

            (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).pressedDetailColor.normalColor;

        }
        else
        {

            (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).detailColor.normalColor;

        }

    }



}