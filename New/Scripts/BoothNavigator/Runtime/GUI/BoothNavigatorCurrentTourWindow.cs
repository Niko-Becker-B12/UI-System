using BoothNavigator;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoothNavigatorCurrentTourWindow : MonoBehaviour
{

    BoothNavigatorManager boothNavigatorManager => FindAnyObjectByType<BoothNavigatorManager>();

    UiWindow uiWindow => this.GetComponent<UiWindow>();

    public GameObject topicEntryPrefab;

    //public List<BoothTopic> currentTourTopics = new List<BoothTopic>();

    public List<UiElement> topicEntries = new List<UiElement>();


    public void AddTopicToList(BoothTopic topic)
    {

        if (boothNavigatorManager.currentTour.AddTopicToTour(topic))    
            AddEntry(topic);

    }

    void RemoveTopic(BoothTopic topic)
    {

        boothNavigatorManager.currentTour.RemoveTopicFromTour(topic);

    }

    void AddEntry(BoothTopic topic)
    {

        GameObject newEntry = Instantiate(topicEntryPrefab, uiWindow.windowBody);

        LayoutRebuilder.ForceRebuildLayoutImmediate(uiWindow.windowBody);

    }

    void RemoveEntry(BoothTopic topic)
    {

        int index = boothNavigatorManager.currentTour.topics.IndexOf(topic);

        RemoveTopic(topic);

        Destroy(topicEntries[index].gameObject);
        topicEntries.RemoveAt(index);

    }

}