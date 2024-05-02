using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiWindowTable : UiWindowModal
{

    public string tableHeader;
    public UiTextSkinDataObject tableHeaderStyle;
    public TextMeshProUGUI tableHeaderText;


    public void Awake()
    {
        
        base.Awake();

    }

    public override void ApplySkinData()
    {

        base.ApplySkinData();

        if (tableHeaderText == null)
        {

            GameObject temp = new GameObject("TableHeaderText", typeof(TextMeshProUGUI));

            tableHeaderText = temp.GetComponent<TextMeshProUGUI>();
            tableHeaderText.transform.parent = this.transform;
            tableHeaderText.transform.SetSiblingIndex(1);
            tableHeaderText.styleSheet = GameManager.Instance.clientSkinDataSets[GameManager.Instance.currentClientIndex].textStyleSheet;
            tableHeaderText.text = tableHeader;

            UiText text = temp.AddComponent<UiText>();

            text.skinData = tableHeaderStyle;
            text.ApplySkinData();

        }
        else if (tableHeaderText != null && Application.isPlaying && GameManager.Instance.currentClientIndex != -1)
        {

            Debug.Log($"{tableHeaderText} {Application.isPlaying} {GameManager.Instance.currentClientIndex}");

            tableHeaderText.styleSheet = GameManager.Instance.clientSkinDataSets[GameManager.Instance.currentClientIndex].textStyleSheet;
            tableHeaderText.text = tableHeader;

            if(!tableHeaderText.TryGetComponent<UiText>(out UiText text))
            {

                text = tableHeaderText.gameObject.AddComponent<UiText>();

                text.skinData = tableHeaderStyle;
                text.backgroundGraphic = tableHeaderText;
                text.ApplySkinData();

            }
            else
            {

                text.skinData = tableHeaderStyle;
                text.backgroundGraphic = tableHeaderText;
                text.ApplySkinData();

            }

        }

    }

}