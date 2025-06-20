using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{

    public static event Action<int> onValidatedLogin;
    public static event Action OnLoginFinalized;
    public static event Action<int> onNewSelectedStream;

    public static event Action onOpenLoadingScreen;
    public static event Action onCloseLoadingScreen;
    
    public int currentAppState;

    public List<AppState> appStates = new List<AppState>();


    private void Start()
    {

        for (int i = 1; i < Display.displays.Length; i++)
        {

            Display.displays[i].Activate();

        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        OnLoginFinalized?.Invoke();

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

    public void Quit()
    {

        Application.Quit();

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