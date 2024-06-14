using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{

    public RawImage streamRenderTexture;

    public int selectedWindow = 0;
    public int lastWindow = -1;
    public List<UiWindow> mainWindows = new List<UiWindow>();

    public UiWindow loadingScreen;

    public RectTransform customFullscreenComponentsParent;

    //might become a UiWindowDraggable
    public UiElement streamPictureInPictureWindow;

    [Header("Players")]
    public GameObject playerToggleButton;
    public Transform playerToggleParent;


    private void Awake()
    {

        base.Awake();

        GameManager.onValidatedLogin += delegate { SwitchWindow(0); };
        GameManager.onValidatedLogin += ApplyClientSkin;

        GameManager.onOpenLoadingScreen += delegate { StartCoroutine(OpenLoadingScreen()); };
        GameManager.onCloseLoadingScreen += CloseLoadingScreen;

    }

    private void Start()
    {

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponentInChildren<RectTransform>());
        Canvas.ForceUpdateCanvases();

    }

    public void ApplyClientSkin()
    {

        //if()

        GameManager.onOpenLoadingScreen -= delegate { StartCoroutine(OpenLoadingScreen()); };

        for(int i = 0; i < mainWindows.Count; i++)
        {

            LayoutRebuilder.ForceRebuildLayoutImmediate(mainWindows[i].backgroundGraphic.rectTransform);

        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponentInChildren<RectTransform>());
        Canvas.ForceUpdateCanvases();

    }

    public void SwitchWindow(int index = 0)
    {

        Canvas.ForceUpdateCanvases();

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

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponentInChildren<RectTransform>());
        Canvas.ForceUpdateCanvases();

        if (streamPictureInPictureWindow != null)
            if (mainWindows[selectedWindow].opensStreamInPiPMode)
                streamPictureInPictureWindow.FadeElement(true);
            else if (!mainWindows[selectedWindow].opensStreamInPiPMode)
                streamPictureInPictureWindow.FadeElement(false);

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