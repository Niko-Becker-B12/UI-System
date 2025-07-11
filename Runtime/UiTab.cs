using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace GPUI
{
    public class UiTab : UiToggle
    {

        [TabGroup("Tabs", "UI Elements")] public Graphic secondaryDetailGraphic;

        [TabGroup("Tabs", "UI Elements")] public LocalizedString title;

        [TabGroup("Tabs", "UI Elements")] [ReadOnly]
        public int tabIndex;

        [TabGroup("Tabs", "UI Elements")] public Transform contentHolderParent;

        [TabGroup("Tabs", "UI Elements")] public UiTabGroup tabGroup;


        public void Awake()
        {

            base.Awake();

        }

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            if (detailGraphic.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
            {

                if (title.IsEmpty)
                    text.text = $"Tab {tabIndex}";
                else
                {

                    title.StringChanged += text.SetText;

                }

            }

            if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
                return;

            if (detailGraphic != null && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                
                secondaryDetailGraphic.color = detailedSkinData.detailColor.normalColor;

            }

        }

        public override void OnClick(bool isActive)
        {


            base.OnClick(isActive);

            if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
                return;

            if (isActive)
            {

                (secondaryDetailGraphic as Image).color =
                    (skinData as UiToggleSkinDataObject).pressedDetailColor.normalColor;

            }
            else
            {

                (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).detailColor.normalColor;

            }

        }

        public override void OnEnter()
        {

            base.OnEnter();

            if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
                return;

            if (_toggleBehavior.isActive)
            {

                (secondaryDetailGraphic as Image).color =
                    (skinData as UiToggleSkinDataObject).pressedDetailColor.highlightedColor;

            }
            else
            {

                (secondaryDetailGraphic as Image).color =
                    (skinData as UiToggleSkinDataObject).detailColor.highlightedColor;

            }

        }

        public override void OnExit()
        {

            base.OnExit();

            if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
                return;

            if (_toggleBehavior.isActive)
            {

                (secondaryDetailGraphic as Image).color =
                    (skinData as UiToggleSkinDataObject).pressedDetailColor.normalColor;

            }
            else
            {

                (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).detailColor.normalColor;

            }

        }

        public override void OnReset()
        {

            base.OnReset();

            if (skinData == null || skinData.GetType() != typeof(UiToggleSkinDataObject))
                return;

            if (_toggleBehavior.isActive)
            {

                (secondaryDetailGraphic as Image).color =
                    (skinData as UiToggleSkinDataObject).pressedDetailColor.normalColor;

            }
            else
            {

                (secondaryDetailGraphic as Image).color = (skinData as UiToggleSkinDataObject).detailColor.normalColor;

            }

        }



    }
}