using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GPUI
{
    public class UiWindow : UiElement
    {

        [TabGroup("Tabs", "UI Elements")] public RectTransform windowHeader;
        
        [FormerlySerializedAs("windowHeader")] [TabGroup("Tabs", "UI Elements")] public LocalizedString windowHeaderLocalizedString;

        [TabGroup("Tabs", "UI Elements")] public UiText windowHeaderText;

        [TabGroup("Tabs", "UI Elements")] public RectTransform windowBody;

        [TabGroup("Tabs", "Custom")] [ReadOnly]
        public int windowId = -1;

        [TabGroup("Tabs", "Events")] public Function OnSetActive;

        [TabGroup("Tabs", "Events")] public Function OnSetInactive;

        Vector2 sizeDelta => rectTransform.sizeDelta;

        Vector2 offsetMin; // => rectTransform.offsetMin;
        Vector2 offsetMax; // => rectTransform.offsetMax;


        protected override void Start()
        {
            
            base.Start();

            offsetMin = rectTransform.offsetMin;
            offsetMax = rectTransform.offsetMax;

            LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic?.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(windowBody);
            
        }

        public override void FadeElement(bool fadeIn = false)
        {

            base.FadeElement(fadeIn);

            if (fadeIn)
            {

                Function.InvokeEvent(OnSetActive, this);

            }
            else
            {

                Function.InvokeEvent(OnSetInactive, this);

            }

        }

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            if (skinData == null)
                return;

            if (windowHeaderText != null)
            {
                
                if (!windowHeaderLocalizedString.IsEmpty || windowHeaderLocalizedString.TableEntryReference.ReferenceType != TableEntryReference.Type.Empty || windowHeaderLocalizedString.TableReference.ReferenceType != TableReference.Type.Empty)
                {
                    windowHeaderText.localizedString = windowHeaderLocalizedString;
                    windowHeaderText.OverrideText(windowHeaderLocalizedString?.GetLocalizedString());
                }
                
                if ((skinData as UiWindowSkinDataObject).windowHeaderSkinData != null)
                {
                    
                    windowHeaderText.skinData = (skinData as UiWindowSkinDataObject).windowHeaderSkinData;
                    windowHeaderText.ApplySkinData();
                    
                    ApplyLayout(windowHeader);

                }

            }
            
            ApplyLayout(windowBody);

            LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic?.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(windowBody);

        }

        public override void ApplyLayout(RectTransform layoutRectTransform)
        {
            base.ApplyLayout(layoutRectTransform);
        }

        public virtual void ToggleFullscreen(bool setActive = false)
        {

            if (setActive)
            {

                //rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;

            }
            else
            {

                //rectTransform.sizeDelta = sizeDelta;

                rectTransform.offsetMin = offsetMin;
                rectTransform.offsetMax = offsetMax;

            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic?.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(windowBody);

        }

    }
}