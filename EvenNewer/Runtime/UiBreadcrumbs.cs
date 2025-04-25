using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GPUI
{
    public class UiBreadcrumbs : UiText
    {

        [InfoBox("First and last will always be displayed!")]
        public int amountOfCrumbsToDisplay = 5;

        public List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();

        [Button]
        public virtual void DisplayBreadcrumbs()
        {

            (backgroundGraphic as TextMeshProUGUI).text = "";

            CreateBreadcrumb(breadcrumbs[0]);

            if (breadcrumbs.Count <= 1)
                return;

            for (int i = 1; i < breadcrumbs.Count - 1; i++)
            {

                if (i > amountOfCrumbsToDisplay)
                {

                    breadcrumbs[i].currentlyTruncated = true;

                }
                else
                {

                    breadcrumbs[i].currentlyTruncated = false;

                }

                CreateBreadcrumb(breadcrumbs[i], true);

            }

            CreateBreadcrumb(breadcrumbs[breadcrumbs.Count - 1], true);

        }

        public virtual void CreateBreadcrumb(Breadcrumb breadcrumb, bool addStartArrow = false)
        {

            TextMeshProUGUI textObject = backgroundGraphic as TextMeshProUGUI;

            if (addStartArrow)
                textObject.text += " > ";

            if (!breadcrumb.currentlyTruncated)
                textObject.text += $"<link=\"{breadcrumb.linkID}\">{breadcrumb.title}</link>";
            else
                textObject.text += $"<link=\"{breadcrumb.linkID}\">...</link>";

        }

        public virtual void AddBreadcrumb(Breadcrumb breadcrumb)
        {

            breadcrumbs.Add(breadcrumb);

        }

        public virtual void AddBreadcrumb(string title)
        {

            breadcrumbs.Add(new Breadcrumb { title = title });

        }

        public virtual void RemoveBreadcrumb(int index)
        {

            breadcrumbs.RemoveAt(index);

        }

        public virtual void RemoveBreadcrumb(string title)
        {

            Breadcrumb breadcrumbToRemove = breadcrumbs.Find(x => x.title == title);

            breadcrumbs.Remove(breadcrumbToRemove);

        }

        public virtual void RemoveBreadcrumb(Breadcrumb breadcrumb)
        {

            int indexToRemove = breadcrumbs.IndexOf(breadcrumb);

            if (indexToRemove != -1)
                breadcrumbs.Remove(breadcrumb);

        }

        [Button]
        public virtual void SwitchBreadcrumb(int index)
        {

            for (int i = breadcrumbs.Count - 1; i >= 0; i--)
            {

                if (i > index)
                    RemoveBreadcrumb(breadcrumbs[i]);
                else
                    break;

            }

            DisplayBreadcrumbs();

        }

        public virtual void SwitchBreadcrumb(string title)
        {

            Breadcrumb foundBreadcrumb = breadcrumbs.Find(x => x.title == title);

            int index = breadcrumbs.IndexOf(foundBreadcrumb);

            if (foundBreadcrumb == null || index == -1)
                return;

            SwitchBreadcrumb(index);

        }

        public virtual void SwitchBreadcrumb(Breadcrumb breadcrumb)
        {

            int index = breadcrumbs.IndexOf(breadcrumb);

            if (breadcrumb == null || index == -1)
                return;

            SwitchBreadcrumb(index);

        }

    }

    [Serializable]
    public class Breadcrumb
    {

        public string title;
        public string linkID;
        [ReadOnly] public bool currentlyTruncated = false;

        public UnityEvent<Breadcrumb> OnBreadcrumbClicked;
        public UnityEvent<Breadcrumb> OnBreadcrumbUpdated;

    }
}