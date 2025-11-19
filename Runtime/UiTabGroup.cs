using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GPUI
{
    public class UiTabGroup : UiElement
    {

        [TabGroup("Tabs", "Custom")] public GameObject tabButtonPrefab;
        [TabGroup("Tabs", "Custom")] public ComponentSkinDataObject tabButtonSkin;
        
        [TabGroup("Tabs", "Custom")] public RectTransform tabButtonParent;
        
        int currentIndex;

        [TabGroup("Tabs", "Custom")] public List<UiTab> tabs = new List<UiTab>();

        [TabGroup("Events", "Custom")] public UnityEvent<int> OnTabSelected;


        public void Awake()
        {

            base.Awake();

        }

        private void OnDisable()
        {

            RemoveAllTabs();

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

        public void AddNewTab(string title = "", Sprite sprite = null, int index = -1)
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

        public void AddNewTabs(string[] titles)
        {

            for (int i = 0; i < titles.Length; i++)
            {

                int index = i;
                
                UiTab temp = GenerateTabButton(titles[i], null, index);
                temp.ApplySkinData();

                tabs.Add(temp);
                
            }
            
        }

        UiTab GenerateTabButton(string title = "", Sprite sprite = null, int index = -1)
        {

            if (index == -1)
                index = tabs.Count;

            GameObject tempButtonObject = Instantiate(tabButtonPrefab, tabButtonParent);
            UiTab tab = tempButtonObject.GetComponent<UiTab>();
            
            tab.tabGroup = this;
            tab.tabIndex = index;


            Function tabButtonOnClickFunction = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            tabButtonOnClickFunction.functionName.AddListener(() => { SwitchTab(index); });

            tab.onSetActiveFunctions.Add(tabButtonOnClickFunction);

            tab._toggleBehavior.clickableOnce = true;

            for (int i = 0; i < tab.secondaryDetailGraphics.Count; i++)
            {

                if (tab.secondaryDetailGraphics[i] is Image)
                {
                    (tab.secondaryDetailGraphics[i] as Image).sprite = sprite;
                    break;
                }
                
            }

            string[] localizationReferences = new string[2];

            if (title.Contains('/'))
            {

                localizationReferences = title.Split('/');

                tab.title.SetReference(localizationReferences[0], localizationReferences[1]);

            }
            else
            {

                (tab.detailGraphic as TextMeshProUGUI).text = title;

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(tab.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(tabButtonParent);

            return tab;

        }

        public void RemoveAllTabs()
        {

            for (int i = tabs.Count - 1; i > -1; i--)
            {

                RemoveTab(i);

            }

        }

        public void RemoveTab(int index)
        {

            //Destroy(tabs[index]?.contentHolderParent?.gameObject);
            Destroy(tabs[index]?.gameObject);

            tabs.RemoveAt(index);

        }

        public void SwitchTab(int index)
        {

            OnTabSelected?.Invoke(index);
            
            for (int i = 0; i < tabs.Count; i++)
            {

                if (i == index)
                {

                    tabs[i].OnClick(true);

                    currentIndex = index;

                }
                else
                {
                    
                    tabs[i]._toggleBehavior.ResetClick();

                    continue;

                }

            }

        }

        public int GetTabIndex(string title)
        {

            return tabs.FindIndex(x => x.title.TableEntryReference == title);

        }

    }
}