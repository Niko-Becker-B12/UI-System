using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof(ContentSizeFitter))]
public class UiText : UiElement
{

    ContentSizeFitter contentSizeFitter => GetComponent<ContentSizeFitter> ();

    public bool fitHorizontalSizeToText = true;
    public bool fitVerticalSizeToText = true;

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

    public override void ApplySkinData()
    {

        if (skinData == null)
            return;

        if(backgroundGraphic != null)
        {

            (backgroundGraphic as TextMeshProUGUI).color = skinData.backgroundColor.normalColor;


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
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.transform.parent as RectTransform);

        }

    }

}