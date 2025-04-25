using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.Events;

namespace GPUI
{
    public class UiContextMenu : UiWindow, IPointerEnterHandler, IPointerExitHandler
    {

        public static UiContextMenu Instance;

        float currentTime;
        float maxTimer = 5f;

        bool mouseOverActive = false;

        public GameObject contextMenuEntryPrefab;

        public List<UiContextMenuEntry> entries = new List<UiContextMenuEntry>();


        private void Awake()
        {

            base.Awake();

            if (Instance == null)
                Instance = this;
            else
                GameObject.Destroy(this.gameObject);

        }

        private void OnDisable()
        {

            RemoveAllEntries();

        }

        private void FixedUpdate()
        {

            if (!mouseOverActive)
            {

                if (currentTime < maxTimer)
                    currentTime += Time.deltaTime;
                else
                {

                    currentTime = 0;

                    FadeElement();

                }

            }

        }

        public override void ApplySkinData()
        {

            base.ApplySkinData();

        }

        public override void FadeElement(bool fadeIn = false)
        {

            if (fadeIn)
            {

                if (setActiveAnimation == null || setActiveAnimation.animation.GetType() == typeof(UiAnimationBase))
                {

                    canvasGroup.DOFade(1f, .5f)
                        .OnStart(() =>
                        {

                            canvasGroup.blocksRaycasts = true;
                            canvasGroup.interactable = true;

                        });

                }
                else
                    setActiveAnimation.Play();

            }
            else
            {

                if (setInactiveAnimation == null || setInactiveAnimation.animation.GetType() == typeof(UiAnimationBase))
                {

                    canvasGroup.DOFade(0f, .5f)
                        .OnStart(() =>
                        {

                            canvasGroup.blocksRaycasts = false;
                            canvasGroup.interactable = false;

                        })
                        .OnComplete(() => { this.gameObject.SetActive(false); });

                }
                else
                    setInactiveAnimation.Play();

            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            mouseOverActive = true;
            currentTime = 0;

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseOverActive = false;
            currentTime = 0;
        }

        public void AddNewMenuEntry(string title = "", int index = -1, List<Function> onClickFunctions = null)
        {

            if (index == -1)
            {

                //Add on the end

                UiContextMenuEntry temp = GenerateMenuEntry(title, index, onClickFunctions);
                temp.ApplySkinData();

                entries.Add(temp);

            }
            else
            {

                //add at index

                UiContextMenuEntry temp = GenerateMenuEntry(title, index, onClickFunctions);
                temp.ApplySkinData();

                entries.Insert(index, temp);

            }

        }

        UiContextMenuEntry GenerateMenuEntry(string title = "", int index = -1, List<Function> onClickFunctions = null)
        {

            if (index == -1)
                index = entries.Count;

            GameObject tempButtonObject = Instantiate(contextMenuEntryPrefab, windowBody);
            UiContextMenuEntry newEntry = tempButtonObject.GetComponent<UiContextMenuEntry>();


            Function tabButtonOnClickFunction = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            tabButtonOnClickFunction.functionName.AddListener(() => { FadeElement(); });

            newEntry.onClickFunctions.Add(tabButtonOnClickFunction);
            newEntry.onClickFunctions.AddRange(onClickFunctions);

            string[] localizationReferences = new string[2];

            if (title.Contains('/'))
            {

                localizationReferences = title.Split('/');

                newEntry.title.SetReference(localizationReferences[0], localizationReferences[1]);

            }
            else
            {

                (newEntry.detailGraphic as TextMeshProUGUI).text = title;

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(newEntry.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(windowBody);

            return newEntry;

        }

        public void RemoveAllEntries()
        {

            for (int i = entries.Count - 1; i > -1; i--)
            {

                RemoveEntry(i);

            }

        }

        public void RemoveEntry(int index)
        {

            Destroy(entries[index].gameObject);

            entries.RemoveAt(index);

        }

    }
}