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
    public RectTransform contentGroupHolder;

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

        if (index == -1)
        {

            //Add on the end

            UiTab temp = GenerateTabButton(title, null, index);
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

    public void AddNewTab(string title = "",Sprite sprite = null,  int index = -1)
    {

        if (index == -1)
        {

            //Add on the end

            UiTab temp = GenerateTabButton(title, sprite, index);
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

    UiTab GenerateTabButton(string title = "", Sprite sprite = null, int index = -1)
    {

        if (index == -1)
            index = tabs.Count;

        GameObject tempButtonObject = Instantiate(tabButtonPrefab, tabButtonParent);
        UiTab tab = tempButtonObject.GetComponent<UiTab>();

        //GameObject tempContentHolderObject = Instantiate(contentGroupPrefab, contentGroupHolder);
        //
        //tab.contentHolderParent = GameObject.Find($"{contentGroupHolder.name}/{tempContentHolderObject.name}{contentPosition}").transform;

        GameObject newContentHolder = Instantiate(contentGroupPrefab, contentGroupHolder);
        newContentHolder.transform.SetParent(contentGroupHolder);
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
        tab.tabGroup = this;
        tab.tabIndex = index;


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

        (tab.secondaryDetailGraphic as Image).sprite = sprite;

        tab.title.TableEntryReference = title;

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

                LayoutRebuilder.ForceRebuildLayoutImmediate(tabs[index].contentHolderParent.GetComponent<RectTransform>());
                LayoutRebuilder.ForceRebuildLayoutImmediate(contentGroupHolder);

                tabs[i].OnClick(true);

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

        Debug.Log($"Moving {rootObject.name} to {index}");

        if (index == -1 || rootObject == null)
        {
            //Error handling
            return;
        }

        Debug.Log($"Moving {rootObject.name} to {tabs[index].contentHolderParent.name}");

        rootObject.transform.SetParent(tabs[index].contentHolderParent);
        rootObject.transform.localScale = Vector3.one;

        LayoutRebuilder.ForceRebuildLayoutImmediate(tabs[index].contentHolderParent.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

    }

    public Transform GetEntryParent(int index)
    {

        return null;

    }

    public int GetTabIndex(string title)
    {

        return tabs.FindIndex(x => x.title.TableEntryReference == title);

    }

}