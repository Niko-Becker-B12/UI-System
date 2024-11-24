using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiWindowModal : UiWindow
{

    [FoldoutGroup("UI Elements")]
    public UiButton closeButton;

    public bool useManagerForClosing = false;


    public override void Awake()
    {

        Function onCloseButton = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onCloseButton.functionName.AddListener(() =>
        {

            if(UiManager.Instance != null && useManagerForClosing)
                UiManager.Instance.GoToLastWindow();
            else if (UiManager.Instance == null || !useManagerForClosing)
                FadeElement();


        });

        closeButton.onClickFunctions.Add(onCloseButton);

        if(this.skinData != null)
        {

            closeButton.skinData = (this.skinData as UiWindowModalSkinDataObject).closeButtonSkinData;
            closeButton.ApplySkinData();

        }

        base.Awake();

    }

    private void Start()
    {
        
    }

    public override void ApplySkinData()
    {

        base.ApplySkinData();

        closeButton.ApplySkinData();

    }

}