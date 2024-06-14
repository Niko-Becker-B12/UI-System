using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiText : UiElement
{

    private void Awake()
    {
        
        base.Awake();

    }

    private void Start()
    {
        


    }

    public override void ApplySkinData()
    {

        Debug.Log($"Apply Skin to Text Object");

        if (skinData == null)
            return;

        if(backgroundGraphic != null)
        {

            (backgroundGraphic as TextMeshProUGUI).color = skinData.backgroundColor.normalColor;


            if(Application.isPlaying && GameManager.Instance != null)
            {

                (backgroundGraphic as TextMeshProUGUI).styleSheet = GameManager.Instance.ClientSkinDataObject.textStyleSheet;

                if (skinData is UiTextSkinDataObject && (skinData as UiTextSkinDataObject).textStyle != "")
                    (backgroundGraphic as TextMeshProUGUI).textStyle = GameManager.Instance.ClientSkinDataObject.textStyleSheet.GetStyle((skinData as UiTextSkinDataObject).textStyle);

            }

            backgroundGraphic.SetAllDirty();
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.transform.parent as RectTransform);

        }

    }

}