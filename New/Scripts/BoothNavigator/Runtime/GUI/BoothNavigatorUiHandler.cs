using BoothNavigator;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoothNavigatorUiHandler : MonoBehaviour
{

    BoothNavigatorManager boothNavigatorManager => FindAnyObjectByType<BoothNavigatorManager>();

    public UiWindow tourWindow;
    public UiTabGroup tourTabGroup;

    public GameObject tourUiElement;
    public GameObject areaGroupUiElement;
    public GameObject boothTopicUiElement;

    public BoothNavigatorCurrentTourWindow currentTourWindow;

    List<UiElement> tourElements = new List<UiElement>();
    List<BoothNavigatorUiAreaEntry> areaGroupElements = new List<BoothNavigatorUiAreaEntry>();

    int currentIndex = -1;


    private void Start()
    {

        boothNavigatorManager.OnTopicAddedToTour += AddTopicToCurrentTour;

        tourTabGroup.AddNewTab("Localization_BoothNavigator/Button_Tours", null, -1);
        tourTabGroup.AddNewTab("Localization_BoothNavigator/Button_Areas", null, -1);

        tourTabGroup.SwitchTab(0);

        for (int i = 0; i < boothNavigatorManager.boothTours.Count; i++)
        {

            GameObject newTourUiElement = Instantiate(tourUiElement);
            tourTabGroup.AddEntryToGroup(newTourUiElement, 0);

            tourElements.Add(newTourUiElement.GetComponent<UiElement>());

        }

        for (int i = 0; i < boothNavigatorManager.boothAreas.Count; i++)
        {

            int boothAreaIndex = i;

            GameObject newAreaUiElement = Instantiate(areaGroupUiElement);
            tourTabGroup.AddEntryToGroup(newAreaUiElement, 1);

            BoothNavigatorUiAreaEntry areaUiToggle = newAreaUiElement.GetComponent<BoothNavigatorUiAreaEntry>();
            areaGroupElements.Add(areaUiToggle);

            Function areaOnSetActiveFunction = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            areaOnSetActiveFunction.functionName.AddListener(() =>
            {

                SelectAreaGroup(boothAreaIndex);

            });
            areaUiToggle.onSetActiveFunctions.Add(areaOnSetActiveFunction);

            Function areaOnSetInactiveFunction = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            areaOnSetInactiveFunction.functionName.AddListener(() =>
            {

                SelectAreaGroup(boothAreaIndex);

            });
            areaUiToggle.onSetDeactiveFunctions.Add(areaOnSetInactiveFunction);


            for (int j = 0; j < boothNavigatorManager.boothAreas[i].topics.Count; j++)
            {

                GameObject newBoothUiElement = Instantiate(boothTopicUiElement, newAreaUiElement.transform.GetChild(1));
                UiButton boothUiButton = newBoothUiElement.GetComponent<UiButton>();


                Function OnClickBoothButton = new Function
                {
                    functionDelay = 0,
                    functionName = new UnityEvent { }
                };
                OnClickBoothButton.functionName.AddListener(() =>
                {

                    SelectAreaGroup(boothAreaIndex);

                });
                boothUiButton.onClickFunctions.Add(OnClickBoothButton);

            }

            newAreaUiElement.transform.GetChild(1).gameObject.SetActive(false);

        }

    }

    public void SelectAreaGroup(int index)
    {

        if (index == currentIndex)
        {

            for (int i = 0; i < areaGroupElements.Count; i++)
            {

                areaGroupElements[i].transform.GetChild(1).gameObject.SetActive(false);

                LayoutRebuilder.ForceRebuildLayoutImmediate(areaGroupElements[i].rectTransform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(tourTabGroup.rectTransform);

            }

            currentIndex = -1;

        }
        else
        {

            currentIndex = index;

            for (int i = 0; i < areaGroupElements.Count; i++)
            {

                if (i == index)
                {

                    areaGroupElements[i].transform.GetChild(1).gameObject.SetActive(true);

                    for(int j = 0; j < areaGroupElements.Count; j++)
                    {



                    }

                }
                else
                {

                    areaGroupElements[i].OnReset();

                    areaGroupElements[i].transform.GetChild(1).gameObject.SetActive(false);

                }

                LayoutRebuilder.ForceRebuildLayoutImmediate(areaGroupElements[i].rectTransform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(tourTabGroup.rectTransform);

            }

        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(tourTabGroup.rectTransform);

    }

    public void AddTopicToCurrentTour(BoothTopic topic)
    {

        currentTourWindow.AddTopicToList(topic);

        UiToastHolder.Instance.CreateNewToast(out int index, $"Added {topic.title} to current tour!", topic.description, UiToastHolder.ToastElementType.success, 10, null, true);
        UiToastHolder.Instance.DisplayToast();

    }

}