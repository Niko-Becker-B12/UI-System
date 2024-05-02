using DG.Tweening;
using FMETP;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{

    public int selectedWindow = 0;
    public int lastWindow = -1;
    public List<UiWindow> mainWindows = new List<UiWindow>();

    public UiWindow loadingScreen;

    public RectTransform customFullscreenComponentsParent;

    public StreamComponentWindow streamWindow;

    [Header("Players")]
    public GameObject playerToggleButton;
    public Transform playerToggleParent;

    public List<PlayerStreamToggleButton> playerToggles = new List<PlayerStreamToggleButton>();

    public static event Action<int> OnWindowSwitch;


    private void Awake()
    {

        base.Awake();

        GameManager.onValidatedLogin += delegate { SwitchWindow(1); };
        GameManager.onValidatedLogin += ApplyClientSkin;

        GameManager.onOpenLoadingScreen += delegate { StartCoroutine(OpenLoadingScreen()); };
        GameManager.onCloseLoadingScreen += CloseLoadingScreen;

    }

    public void ApplyClientSkin(int clientSkinIndex)
    {

        GameManager.onOpenLoadingScreen -= delegate { StartCoroutine(OpenLoadingScreen()); };

        for(int i = 0; i < mainWindows.Count; i++)
        {

            LayoutRebuilder.ForceRebuildLayoutImmediate(mainWindows[i].backgroundGraphic.rectTransform);

        }


    }

    public void GenerateNewConnectedPlayerToggle(int playerID, out PlayerStreamToggleButton toggle)
    {

        PlayerStreamToggleButton newPlayerStreamToggle = Instantiate(playerToggleButton, playerToggleParent).GetComponent<PlayerStreamToggleButton>();
        newPlayerStreamToggle.connectedPlayerId = playerID;

        playerToggles.Add(newPlayerStreamToggle);

        toggle = newPlayerStreamToggle;

    }

    public void SwitchWindow(int index = 0)
    {

        lastWindow = selectedWindow;
        selectedWindow = index;

        if (lastWindow == -1)
            lastWindow = selectedWindow;

        Debug.Log($"Switching Window: {selectedWindow} ({lastWindow})");

        for(int i = 0; i < mainWindows.Count; i++)
        {

            int currentIndex = i;

            if (currentIndex == index)
            {

                mainWindows[currentIndex].FadeElement(true);

            }
            else
            {

                mainWindows[currentIndex].FadeElement(false);

                continue;

            }

        }

        OnWindowSwitch.Invoke(index);

    }

    public void GoToLastWindow()
    {

        SwitchWindow(lastWindow);

    }

    IEnumerator OpenLoadingScreen()
    {

        loadingScreen.FadeElement(true);

        //To-Do: create actual loadingscreen, right now we'll wait for 5s

        yield return new WaitForSecondsRealtime(7f);

        GameManager.Instance.LoadingFinished();

    }

    void CloseLoadingScreen()
    {

        loadingScreen.FadeElement(false);

    }

}