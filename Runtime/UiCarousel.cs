using System;
using System.Collections;
using System.Collections.Generic;
using LitMotion;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GPUI
{
    public class UiCarousel : UiElement
    {

        [SerializeField, ReadOnly] public int currentIndex = -1;

        public int stepSize = 1;

        [Space] [TabGroup("Tabs", "UI Elements")]
        public Graphic windowBody;

        private float spacing;

        public List<UiElement> elements = new List<UiElement>();


        public virtual void Start()
        {

            MoveToIndex(0);

            spacing = windowBody.GetComponent<UiCombiLayoutGroup>().spacing;

            LayoutRebuilder.ForceRebuildLayoutImmediate(windowBody.rectTransform);

        }

        public virtual void AddElementToCarousel(UiElement element, int index = -1)
        {

            if (index == -1)
            {

                elements.Add(element);

            }
            else
            {

                elements.Insert(index, element);

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(windowBody.rectTransform);

        }

        [Button]
        public virtual void MoveToIndex(int index)
        {

            currentIndex = index;

            UiElement elementSetActive = null;

            Vector2 newBodyPosition = Vector2.zero;

            for (int i = 0; i < elements.Count; i++)
            {

                if (i < index)
                    newBodyPosition += new Vector2(elements[i].rectTransform.rect.width, 0);

                if (i == index)
                {
                    elementSetActive = elements[i];
                    continue;
                }
                else
                {
                    //elements[i].FadeElement();
                }

            }

            if (elementSetActive != null)
            {
                //newBodyPosition += new Vector2((elementSetActive.rectTransform.sizeDelta.x) * index + spacing, 0);
                newBodyPosition += new Vector2(spacing * index, 0);
                elementSetActive.FadeElement(true);
            }
            
            LMotion.Create(windowBody.rectTransform.anchoredPosition, -newBodyPosition, 0.5f)
                .WithEase(Ease.InOutSine);

        }

        [Button]
        public void MoveToNextIndex()
        {

            if (stepSize > 1)
            {

                if (currentIndex + stepSize < elements.Count - 1)
                    currentIndex += stepSize;
                //else if (currentIndex < elements.Count - 1)
                //    currentIndex++;
                else
                    return;

            }
            else
            {

                if (currentIndex < elements.Count - 1)
                    currentIndex++;
                else
                    return;

            }

            MoveToIndex(currentIndex);

        }

        [Button]
        public void MoveToPreviousIndex()
        {

            if (stepSize > 1)
            {

                if (currentIndex - stepSize >= 0)
                    currentIndex -= stepSize;
                //else if (currentIndex > 0)
                //    currentIndex--;
                else
                    return;

            }
            else
            {

                if (currentIndex > 0)
                    currentIndex--;
                else
                    return;

            }

            MoveToIndex(currentIndex);

        }

    }
}