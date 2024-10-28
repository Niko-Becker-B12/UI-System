using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiToastHolder : UiElement
{

    public static UiToastHolder Instance;

    public ComponentSkinDataObject messageSkinData;
    public ComponentSkinDataObject errorSkinData;
    public ComponentSkinDataObject successSkinData;
    public ComponentSkinDataObject warningSkinData;

    public GameObject toastElementPrefab;

    public enum ToastElementType
    {

        message,
        error,
        success,
        warning

    }

    public Queue<UiToastElement> toastQueue = new Queue<UiToastElement>();

    public event Action<UiToastElement> OnToastCurrentlyActive;
    public event Action OnNoToastActive;

    bool isCurrentlyDisplayingToast = false;


    private void Awake()
    {
        
        base.Awake();

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

    }

    public void CreateNewToast(out int index, string title = "", string message = "", ToastElementType toastType = ToastElementType.message, float lifetime = 0, Sprite customIcon = null, bool hasCloseButton = true)
    {

        UiToastElement newUiToast = Instantiate(toastElementPrefab, this.transform).GetComponent<UiToastElement>();

        toastQueue.Enqueue(newUiToast);

        newUiToast.lifetime = lifetime;

        (newUiToast.icon as Image).sprite = customIcon;

        newUiToast.closeButton.gameObject.SetActive(hasCloseButton);

        (newUiToast.headerText.backgroundGraphic as TextMeshProUGUI).text = title;
        (newUiToast.messageText.backgroundGraphic as TextMeshProUGUI).text = message;

        switch(toastType)
        {
            case ToastElementType.message:
                newUiToast.skinData = messageSkinData;
                break;
            case ToastElementType.error:
                newUiToast.skinData = errorSkinData;
                break;
            case ToastElementType.success:
                newUiToast.skinData = successSkinData;
                break;
            case ToastElementType.warning:
                newUiToast.skinData = warningSkinData;
                break;
        }

        newUiToast.ApplySkinData();

        newUiToast.canvasGroup.alpha = 0f;
        newUiToast.canvasGroup.interactable = false;
        newUiToast.canvasGroup.blocksRaycasts = false;

        index = toastQueue.Count;

        newUiToast.OnToastInactive += DisplayToast;

        //if(!isCurrentlyDisplayingToast)
        //    DisplayToast();

        //if (toastQueue.Count > 1)
        //    newUiToast.FadeElement();
        //else if (toastQueue.Count == 1)
        //    DisplayToast();

    }

    public void DisplayToastByIndex(int index = -1)
    {

        if (toastQueue.ElementAt(index) == null)
            return;

    }

    [Button]
    public void DisplayToast()
    {

        if(toastQueue.Count == 0 || isCurrentlyDisplayingToast)
        {

            OnNoToastActive?.Invoke();

            return;

        }

        if(toastQueue.TryPeek(out UiToastElement uiToastElement))// && !isCurrentlyDisplayingToast)
        {

            Debug.Log(uiToastElement.headerText.GetComponent<TextMeshProUGUI>().text);

            isCurrentlyDisplayingToast = true;

            uiToastElement.FadeElement(true);

            OnToastCurrentlyActive?.Invoke(uiToastElement);

            toastQueue.Dequeue();

        }
        else
        {

            toastQueue.Dequeue();

            isCurrentlyDisplayingToast = false;

            DisplayToast();

            return;

        }

    }

}