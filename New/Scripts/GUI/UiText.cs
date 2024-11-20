using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;


[RequireComponent (typeof(ContentSizeFitter))]
public class UiText : UiElement
{

    ContentSizeFitter contentSizeFitter => GetComponent<ContentSizeFitter> ();

    public bool fitHorizontalSizeToText = true;
    public bool fitVerticalSizeToText = true;
    
    public LocalizedString localizedString;

    private void Awake()
    {

        if(fitHorizontalSizeToText)
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        else
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        if(fitVerticalSizeToText)
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        else
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

        base.Awake();

    }

    private void Start()
    {
        


    }

    private void OnEnable()
    {

        localizedString.StringChanged += OverrideText;

    }

    private void OnDisable()
    {
        
        localizedString.StringChanged -= OverrideText;
        
    }

    public virtual void OverrideText(string newText)
    {

        if (!backgroundGraphic.TryGetComponent(out TextMeshProUGUI text))
            return;
        
        text.text = newText;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

    }

    public override void ApplySkinData()
    {

        if (skinData == null)
            return;

        if(backgroundGraphic != null && backgroundGraphic.TryGetComponent(out TextMeshProUGUI text))
        {

            text.color = skinData.backgroundColor.normalColor;


            //if(Application.isPlaying && GameManager.Instance != null && GameManager.Instance.currentClientIndex != -1)
            //{
            //
            //    (backgroundGraphic as TextMeshProUGUI).styleSheet = GameManager.Instance.clientSkinDataSets[GameManager.Instance.currentClientIndex].textStyleSheet;
            //
            //    if (skinData is UiTextSkinDataObject && (skinData as UiTextSkinDataObject).textStyle != "")
            //        (backgroundGraphic as TextMeshProUGUI).textStyle = GameManager.Instance.clientSkinDataSets[GameManager.Instance.currentClientIndex].textStyleSheet.GetStyle((skinData as UiTextSkinDataObject).textStyle);
            //
            //}

            backgroundGraphic.SetAllDirty();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.transform.parent as RectTransform);

        }

    }

}