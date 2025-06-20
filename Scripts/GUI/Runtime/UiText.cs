using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;


namespace GPUI
{
    [RequireComponent(typeof(ContentSizeFitter))]

    public class UiText : UiElement
    {

        ContentSizeFitter contentSizeFitter => GetComponent<ContentSizeFitter>();

        public bool fitHorizontalSizeToText = true;
        public bool fitVerticalSizeToText = true;

        public LocalizedString localizedString;

        private void Awake()
        {

            if (fitHorizontalSizeToText)
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            else
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

            if (fitVerticalSizeToText)
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            else
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

            base.Awake();

        }

        private void Start()
        {



        }

        private void OnEnable()
        {

            localizedString.StringChanged += OverrideText;

        }

        private void OnDisable()
        {

            localizedString.StringChanged -= OverrideText;

        }

        public virtual void OverrideText(string newText)
        {

            if (!backgroundGraphic.TryGetComponent(out TextMeshProUGUI text))
                return;

            text.text = newText;

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        }

        public override void ApplySkinData()
        {

            if (skinData == null)
                return;

            if (backgroundGraphic != null && backgroundGraphic.TryGetComponent(out TextMeshProUGUI text))
            {

                text.color = skinData.backgroundColor.normalColor;

                if (skinData is UiTextSkinDataObject)
                {

                    text.styleSheet = (skinData as UiTextSkinDataObject).styleSheet;
                    text.textStyle =
                        (skinData as UiTextSkinDataObject).styleSheet.GetStyle((skinData as UiTextSkinDataObject)
                            .usedTextStyle);

                }

                backgroundGraphic.SetAllDirty();
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.transform.parent as RectTransform);

            }

        }

    }
}