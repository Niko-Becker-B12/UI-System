using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiTabGroup : UiElement
{

    public GameObject tabButtonPrefab;

    public Transform tabButtonParent;

    public GameObject contentGroupPrefab;
    public Transform contentGroupHolder;

    public string contentPosition;


    int currentIndex;

    public List<UiTab> tabs = new List<UiTab>();


    public void Awake()
    {

        base.Awake();

    }

    private void OnDisable()
    {

        RemoveAlltabs();

    }

    public override void ApplySkinData()
    {

        base.ApplySkinData();



    }

    public void AddNewTab(string title = "", int index = -1)
    {

        if(index == -1)
        {

            //Add on the end

            UiTab temp = GenerateTabButton(title, index);
            temp.ApplySkinData();

            tabs.Add(temp);

        }
        else
        {

            //add at index

            UiTab temp = GenerateTabButton(title);
            temp.ApplySkinData();

            tabs.Insert(index, temp);

        }

    }

    UiTab GenerateTabButton(string title = "", int index = -1)
    {

        if (index == -1)
            index = tabs.Count;

        GameObject tempButtonObject = Instantiate(tabButtonPrefab, tabButtonParent);
        UiTab tab = tempButtonObject.GetComponent<UiTab>();

        //GameObject tempContentHolderObject = Instantiate(contentGroupPrefab, contentGroupHolder);
        //
        //tab.contentHolderParent = GameObject.Find($"{contentGroupHolder.name}/{tempContentHolderObject.name}{contentPosition}").transform;

        GameObject newContentHolder = Instantiate(contentGroupPrefab, contentGroupHolder);
        newContentHolder.transform.parent = contentGroupHolder;
        //newContentHolder.transform.localScale = Vector3.one;
        //
        //VerticalLayoutGroup layoutGroup = newContentHolder.AddComponent<VerticalLayoutGroup>();
        //ContentSizeFitter sizeFitter = newContentHolder.AddComponent<ContentSizeFitter>();
        //
        //
        //layoutGroup.childControlHeight = false;
        //layoutGroup.childControlWidth = true;
        //layoutGroup.childForceExpandHeight = false;
        //layoutGroup.childForceExpandWidth = true;
        //layoutGroup.childScaleHeight = true;
        //layoutGroup.childScaleWidth = false;
        //
        //sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        //sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;


        tab.contentHolderParent = newContentHolder.transform;
        tab.contentHolderParent.gameObject.SetActive(false);


        Function tabButtonOnClickFunction = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        tabButtonOnClickFunction.functionName.AddListener(() =>
        {

            SwitchTab(index);

        });

        tab.onSetActiveFunctions.Add(tabButtonOnClickFunction);

        tab._toggleBehavior.clickableOnce = true;


        tab.title = title;

        return tab;

    }

    public void RemoveAlltabs()
    {

        for (int i = tabs.Count - 1; i > -1; i--)
        {

            RemoveTab(i);

        }

    }

    public void RemoveTab(int index)
    {

        Destroy(tabs[index].contentHolderParent.gameObject);
        Destroy(tabs[index].gameObject);

        tabs.RemoveAt(index);

    }

    public void SwitchTab(int index)
    {

        for(int i = 0; i < tabs.Count; i++)
        {

            if(i == index)
            {

                tabs[i].contentHolderParent.gameObject.SetActive(true);

            }
            else
            {

                tabs[i].contentHolderParent.gameObject.SetActive(false);
                tabs[i]._toggleBehavior.ResetClick();

                continue;

            }

        }

    }

    public void AddEntryToGroup(GameObject rootObject, int index = -1)
    {

        if(index == -1 || rootObject == null)
        {
            //Error handling
            return;
        }

        rootObject.transform.parent = tabs[index].contentHolderParent;
        rootObject.transform.localScale = Vector3.one;

    }

    public Transform GetEntryParent(int index)
    {

        return null;

    }

    public int GetTabIndex(string title)
    {

        return tabs.FindIndex(x => x.title == title);

    }

}