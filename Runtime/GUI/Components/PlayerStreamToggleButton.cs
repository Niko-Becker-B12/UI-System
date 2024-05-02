using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStreamToggleButton : UiToggle
{

    public int connectedPlayerId = -1;

    public TextMeshProUGUI text;


    private void Awake()
    {
        base.Awake();

        GameManager.onPlayerNewCustomID += UpdateInfo;

    }

    private void Start()
    {

        Function onSetActive = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onSetActive.functionName.AddListener(() =>
        {
            ToggleFunction();
        });

        onSetActiveFunctions.Add(onSetActive);

        Function onSetDeactive = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onSetDeactive.functionName.AddListener(() =>
        {
            ToggleFunction();
        });

        onSetDeactiveFunctions.Add(onSetDeactive);


        base.Start();

    }

    public override void ApplySkinData()
    {

        base.ApplySkinData();

        UpdateInfo(connectedPlayerId, $"VR {(connectedPlayerId + 1).ToString("00")}");

    }

    public void UpdateInfo(int playerId, string name)
    {

        if (playerId == -1 || playerId == connectedPlayerId)
        {

            text.text = $"{name}";

            connectedPlayerId = playerId;

        }
        else
            return;

    }

    public void ToggleFunction()
    {

        Debug.Log($"Toggled Stream {connectedPlayerId} to {_toggleBehavior.isActive}");
        GameManager.Instance.SelectPlayerForStream(_toggleBehavior.isActive, connectedPlayerId);

        for(int i = 0; i < UiManager.Instance.playerToggles.Count; i++)
        {

            if (UiManager.Instance.playerToggles[i] == this)
                continue;
            else
            {

                UiManager.Instance.playerToggles[i]._toggleBehavior.isActive = false;
                UiManager.Instance.playerToggles[i].OnClick(false);

            }

        }

    }

}