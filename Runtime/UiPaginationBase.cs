using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GPUI
{
    public class UiPaginationBase : UiElement
    {

        [ShowInInspector, ReadOnly] private int currentIndex = -1;

        List<UiElement> pageQueue = new List<UiElement>();


        public virtual void SwitchPage(int page)
        {

            if (page < 0 || page > pageQueue.Count - 1)
                return;

            currentIndex = page;

            for (int i = 0; i < pageQueue.Count; i++)
            {

                if (i == currentIndex)
                    pageQueue[i].FadeElement(true);
                else
                    pageQueue[i].FadeElement();

            }

        }

        [Button]
        public virtual void MoveToNextPage()
        {

            if (currentIndex < pageQueue.Count - 1)
                currentIndex++;

            SwitchPage(currentIndex);

        }

        [Button]
        public virtual void MoveToPreviousPage()
        {

            if (currentIndex > 0)
                currentIndex--;

            SwitchPage(currentIndex);

        }

        public virtual void CreateNewPage()
        {



        }

    }
}