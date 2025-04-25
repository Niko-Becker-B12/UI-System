using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GPUI
{
    public class UiTooltip : UiElementExtended
    {
    
        [TabGroup("Tabs", "Custom")]
        [ReadOnly]
        public string tooltipText;

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            if (detailGraphic == null)
                return;

            if(detailGraphic.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
            {

                text.color = skinData.detailColor.normalColor;
                text.text = tooltipText;

                LayoutRebuilder.ForceRebuildLayoutImmediate(detailGraphic.rectTransform);

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        }

    }
}