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

    [FoldoutGroup("UI Elements")]
    public LocalizedString windowHeader;

    [FoldoutGroup("UI Elements")]
    public TextMeshProUGUI windowHeaderText;

    [FoldoutGroup("UI Elements")]
    public RectTransform windowBody;

    [FoldoutGroup("Settings")]
    public int windowId = -1;

    [FoldoutGroup("Events")]
    public Function OnSetActive;

    [FoldoutGroup("Events")]
    public Function OnSetInactive;

    Vector2 sizeDelta => rectTransform.sizeDelta;

    Vector2 offsetMin;// => rectTransform.offsetMin;
    Vector2 offsetMax;// => rectTransform.offsetMax;


    private void Start()
    {
        
        offsetMin = rectTransform.offsetMin;
        offsetMax = rectTransform.offsetMax;

    }

    public override void Awake()
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

        if (skinData == null)
            return;

        if(windowHeaderText != null)
        {
            if(windowHeaderText.TryGetComponent<LocalizeStringEvent>(out LocalizeStringEvent localizeString))
            {

                localizeString.SetTable(windowHeader.TableReference.TableCollectionName);
                localizeString.StringReference = windowHeader;

            }

            if (windowHeaderText.TryGetComponent<UiText>(out UiText uitext) && (skinData as UiWindowSkinDataObject).windowHeaderSkinData != null)
            {

                uitext.skinData = (skinData as UiWindowSkinDataObject).windowHeaderSkinData;
                uitext.ApplySkinData();

            }

        }

        if(backgroundGraphic != null) 
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.rectTransform);

    }

    public virtual void ToggleFullscreen(bool setActive = false)
    {

        if (setActive)
        {

            //rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

        }
        else
        {

            //rectTransform.sizeDelta = sizeDelta;

            rectTransform.offsetMin = offsetMin;
            rectTransform.offsetMax = offsetMax;

        }

    }

}