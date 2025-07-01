using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GPUI
{
    public class UiTooltip : UiElement
    {
    
        [TabGroup("Tabs", "Custom")]
        [ReadOnly]
        public string tooltipText;
        
        public UiText tooltipTextObject;

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            if (tooltipTextObject == null)
                return;

            if (tooltipTextObject != null && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                tooltipTextObject.backgroundGraphic.color = detailedSkinData.detailColor.normalColor;
                
                tooltipTextObject?.OverrideText(tooltipText);

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        }

    }
}