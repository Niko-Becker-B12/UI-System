using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTab : UiToggle
{

    public string title;

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

            if (string.IsNullOrWhiteSpace(title))
                title = $"Tab {tabIndex}";

            text.text = title;

        }

    }

}