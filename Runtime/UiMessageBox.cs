using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace GPUI
{
    public class UiMessageBox : UiElement
    {

        //Header

        [TabGroup("Tabs/UI Elements/MessageBox", "Header")]
        public UiCombiLayoutGroup headerLayoutGroup;

        [TabGroup("Tabs/UI Elements/MessageBox", "Header")]
        public bool hasHeader;

        [TabGroup("Tabs/UI Elements/MessageBox", "Header")] [ShowIf("hasHeader")]
        public UiText headerText;

        [TabGroup("Tabs/UI Elements/MessageBox", "Header")] [ShowIf("hasHeader")]
        public LocalizedString headerLocalizedString;

        [TabGroup("Tabs/UI Elements/MessageBox", "Header")] [ShowIf("hasHeader")]
        public bool hasHeaderIcon;

        [TabGroup("Tabs/UI Elements/MessageBox", "Header")] [ShowIf("@hasHeader == true && hasHeaderIcon == true")]
        public ComponentSkinDataObject headerIconSkinData;

        [TabGroup("Tabs/UI Elements/MessageBox", "Header")] [ShowIf("@hasHeader == true && hasHeaderIcon == true")]
        public UiElementExtended headerIcon;


        //Message

        [TabGroup("Tabs/UI Elements/MessageBox", "Message")]
        public UiCombiLayoutGroup messageLayoutGroup;

        [TabGroup("Tabs/UI Elements/MessageBox", "Message")]
        public ComponentSkinDataObject messageSkinData;

        [TabGroup("Tabs/UI Elements/MessageBox", "Message")]
        public UiText messageText;

        [TabGroup("Tabs/UI Elements/MessageBox", "Message")]
        public LocalizedString messageLocalizedString;

        [TabGroup("Tabs/UI Elements/MessageBox", "Message")]
        public bool hasMessageIcon;

        [TabGroup("Tabs/UI Elements/MessageBox", "Message")] [ShowIf("hasMessageIcon")]
        public ComponentSkinDataObject messageIconSkinData;

        [TabGroup("Tabs/UI Elements/MessageBox", "Message")] [ShowIf("hasMessageIcon")]
        public UiElementExtended messageIcon;


        //Buttons

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")]
        public UiCombiLayoutGroup buttonsLayoutGroup;

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")]
        public bool hasPrimaryButton;

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")] [ShowIf("hasPrimaryButton")]
        public UiButton primaryButton;

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")] [ShowIf("hasPrimaryButton")]
        public ComponentSkinDataObject primaryButtonSkinData;

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")] [ShowIf("hasSecondaryButton")]
        public LocalizedString primaryButtonLocalizedString;

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")]
        public bool hasSecondaryButton;

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")] [ShowIf("hasSecondaryButton")]
        public UiButton secondaryButton;

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")] [ShowIf("hasSecondaryButton")]
        public ComponentSkinDataObject secondaryButtonSkinData;

        [TabGroup("Tabs/UI Elements/MessageBox", "Buttons")] [ShowIf("hasSecondaryButton")]
        public LocalizedString secondaryButtonLocalizedString;

        [TabGroup("Tabs", "Events")] public Function OnSetActive;

        [TabGroup("Tabs", "Events")] public Function OnSetInactive;
        

        protected override void Start()
        {

            base.Start();
            
        }

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            headerLayoutGroup.gameObject.SetActive(hasHeader);

            if (skinData != null && skinData is UiWindowSkinDataObject)
                headerText.skinData = (skinData as UiWindowSkinDataObject).windowHeaderSkinData;
            else if (skinData != null)
            {

                headerText.skinData = null;
                headerText.backgroundGraphic.color = (skinData as ComponentSkinDataObject).detailColor.normalColor;

            }

            if (headerLocalizedString != null ||
                !string.IsNullOrWhiteSpace(headerLocalizedString?.GetLocalizedString()))
                headerText.localizedString = headerLocalizedString;

            headerText.ApplySkinData();
            headerText.OverrideText(headerText.localizedString.GetLocalizedString());

            messageText.skinData = messageSkinData;

            if (messageSkinData != null)
                messageText.skinData = messageSkinData;
            else if (skinData != null)
            {

                messageText.skinData = null;
                messageText.backgroundGraphic.color =
                    (messageSkinData as ComponentSkinDataObject).detailColor.normalColor;

            }

            if (messageLocalizedString != null ||
                !string.IsNullOrWhiteSpace(messageLocalizedString?.GetLocalizedString()))
                messageText.localizedString = messageLocalizedString;

            messageText.ApplySkinData();
            messageText.OverrideText(messageText.localizedString.GetLocalizedString());

            headerIcon.skinData = headerIconSkinData;
            headerIcon.ApplySkinData();
            headerIcon.gameObject.SetActive(hasHeaderIcon);

            messageIcon.skinData = messageIconSkinData;
            messageIcon.ApplySkinData();
            messageIcon.gameObject.SetActive(hasMessageIcon);

            if (primaryButton.detailGraphic.TryGetComponent<UiText>(out UiText primaryButtonUiText) &&
                primaryButtonLocalizedString != null ||
                !string.IsNullOrWhiteSpace(primaryButtonLocalizedString?.GetLocalizedString()))
            {

                primaryButtonUiText.localizedString = primaryButtonLocalizedString;

                primaryButtonUiText.ApplySkinData();
                primaryButtonUiText.OverrideText(primaryButtonUiText.localizedString.GetLocalizedString());

            }

            primaryButton.skinData = primaryButtonSkinData;
            primaryButton.ApplySkinData();
            primaryButton.gameObject.SetActive(hasPrimaryButton);

            secondaryButton.skinData = secondaryButtonSkinData;
            secondaryButton.ApplySkinData();
            secondaryButton.gameObject.SetActive(hasSecondaryButton);

            if (secondaryButton.detailGraphic.TryGetComponent<UiText>(out UiText secondaryButtonUiText) &&
                secondaryButtonLocalizedString != null ||
                !string.IsNullOrWhiteSpace(secondaryButtonLocalizedString?.GetLocalizedString()))
            {

                secondaryButtonUiText.localizedString = secondaryButtonLocalizedString;

                secondaryButtonUiText.ApplySkinData();
                secondaryButtonUiText.OverrideText(secondaryButtonUiText.localizedString.GetLocalizedString());

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(headerText.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(messageText.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.rectTransform);

            backgroundGraphic.SetAllDirty();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        }

    }
}