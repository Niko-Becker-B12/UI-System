using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UiWindow : UiElement
{

    [FoldoutGroup("UI Skin")]
    public ComponentSkinDataObject windowHeaderSkinData;

    [FoldoutGroup("UI Elements")]
    public LocalizedString windowHeader;

    [FoldoutGroup("UI Elements")]
    public TextMeshProUGUI windowHeaderText;

    [FoldoutGroup("Settings")]
    public bool opensStreamInPiPMode = false;

    [FoldoutGroup("Settings")]
    public int windowId = -1;

    [FoldoutGroup("Events")]
    public Function OnSetActive;

    [FoldoutGroup("Events")]
    public Function OnSetInactive;


    public void Awake()
    {
        
        base.Awake();

        

    }

    public override void FadeElement(bool fadeIn = false)
    {

        base.FadeElement(fadeIn);

        if (fadeIn)
        {

            Function.InvokeEvent(OnSetActive, this);

        }
        else
        {

            Function.InvokeEvent(OnSetInactive, this);

        }

    }

    public override void ApplySkinData()
    {

        base.ApplySkinData();

        if(windowHeaderText != null)
        {
            if(windowHeaderText.TryGetComponent<LocalizeStringEvent>(out LocalizeStringEvent localizeString))
            {

                localizeString.SetTable(windowHeader.TableReference.TableCollectionName);
                localizeString.StringReference = windowHeader;

            }

            if(windowHeaderText.TryGetComponent<UiText>(out UiText uitext))
            {

                uitext.skinData = windowHeaderSkinData;
                uitext.ApplySkinData();

            }

        }

        if(backgroundGraphic != null) 
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.rectTransform);

    }

}