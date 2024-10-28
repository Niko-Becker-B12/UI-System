using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    #region Singleton

    public static UiManager Instance;

    #endregion


    public int selectedWindow = 0;
    public int lastWindow = -1;

    [ShowInInspector]
    public Stack<int> lastWindows = new Stack<int>(128);

    public List<UiWindow> mainWindows = new List<UiWindow>();


    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        lastWindows = new Stack<int>(128);

    }

    private void Start()
    {

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponentInChildren<RectTransform>());
        Canvas.ForceUpdateCanvases();

    }

    public void ApplyClientSkin(int clientSkinIndex)
    {

        //if()

        //GameManager.onOpenLoadingScreen -= delegate { StartCoroutine(OpenLoadingScreen()); };

        for(int i = 0; i < mainWindows.Count; i++)
        {

            LayoutRebuilder.ForceRebuildLayoutImmediate(mainWindows[i].backgroundGraphic.rectTransform);

        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponentInChildren<RectTransform>());
        Canvas.ForceUpdateCanvases();

    }

    [Button]
    public void SwitchWindow(int index = 0)
    {

        Canvas.ForceUpdateCanvases();



        lastWindows.Push(selectedWindow);

        if (lastWindows.Count > 0 && lastWindows.Peek() == lastWindow)
            lastWindows.Pop();

        lastWindow = selectedWindow;
        selectedWindow = index;

        if (lastWindows.Count > 0 && lastWindows.Peek() == -1)
            lastWindows.Push(selectedWindow);

        if(lastWindows.Count > 0)
            Debug.Log($"Switching Window: {selectedWindow} ({lastWindows.Peek()})");
        else
            Debug.Log($"Switching Window: {selectedWindow} ({index})");

        for (int i = 0; i < mainWindows.Count; i++)
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

    }

    public void GoToLastWindow()
    {

        if (lastWindows.Count == 0)
            return;

        int oldIndex = lastWindows.Pop();
        lastWindow = selectedWindow;

        SwitchWindow(oldIndex);

    }

}