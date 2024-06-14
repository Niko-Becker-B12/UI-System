using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.ShapeUtils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonPersistent<GameManager>
{

    [SerializeField]
    private SkinDataObject _clientSkinDataObject;

    public SkinDataObject ClientSkinDataObject// = new List<SkinDataObject>();
    {

        get
        {
            return _clientSkinDataObject;
        }

        private set
        {
            _clientSkinDataObject = value;
        }
    }

    public static event Action onValidatedLogin;

    public static event Action onOpenLoadingScreen;
    public static event Action onCloseLoadingScreen;



    public int currentAppState;

    public List<AppState> appStates = new List<AppState>();


    private void Start()
    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        onValidatedLogin?.Invoke();

        SwitchAppState(0);

    }

    public void LoadingFinished()
    {

        onCloseLoadingScreen?.Invoke();

    }

    public void SwitchAppState(int index)
    {

        currentAppState = index;

        for (int i = 0; i < appStates.Count; i++)
        {

            if(i == index)
                appStates[index].SwitchToState();
            else
                appStates[i].OnReset();

        }

    }

}

[System.Serializable]
public class AppState
{


    [FoldoutGroup("$stateName")]
    public string stateName;

    [FoldoutGroup("$stateName")]
    [TextArea(5, 10)]
    public string stateDescription;

    [FoldoutGroup("$stateName")]
    public List<Function> onSetActiveFunctions = new List<Function>();

    [FoldoutGroup("$stateName")]
    public List<Function> onResetFunctions = new List<Function>();


    public void SwitchToState()
    {

        Debug.Log("Switching State");

        for(int i = 0; i < onSetActiveFunctions.Count; i++)
            Function.InvokeEvent(onSetActiveFunctions[i], GameManager.Instance);

    }

    public void OnReset()
    {

        Debug.Log($"Resetting State {stateName}");

        for (int i = 0; i < onResetFunctions.Count; i++)
            Function.InvokeEvent(onResetFunctions[i], GameManager.Instance);

    }

}