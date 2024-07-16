using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiWindowModal : UiWindow
{

    [FoldoutGroup("UI Elements")]
    public UiButton closeButton;


    public void Awake()
    {

        base.Awake();

        Function onCloseButton = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onCloseButton.functionName.AddListener(() =>
        {

            if(UiManager.Instance != null)
                UiManager.Instance.GoToLastWindow();
            else
                FadeElement();


        });

        closeButton.onClickFunctions.Add(onCloseButton);

        if(this.skinData != null)
            closeButton.skinData = (this.skinData as UiWindowModalSkinDataObject).closeButtonSkinData;

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