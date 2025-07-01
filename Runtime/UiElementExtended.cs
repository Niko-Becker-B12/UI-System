using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GPUI
{
    public class UiElementExtended : UiElement
    {

        [TabGroup("Tabs", "UI Elements")] public Graphic detailGraphic;

        public override void ApplySkinData()
        {

            base.ApplySkinData();

            if (skinData == null)
                return;

            if (detailGraphic != null && skinData is ComponentSkinDataObject)
            {

                ComponentSkinDataObject detailedSkinData = skinData as ComponentSkinDataObject;
                detailGraphic.color = detailedSkinData.detailColor.normalColor;

                if (detailGraphic is Image)
                {

                    if (detailedSkinData.detailSprite != null)
                        (detailGraphic as Image).sprite = detailedSkinData.detailSprite;
                    if (detailedSkinData.detailMaterial != null)
                        detailGraphic.material = detailedSkinData.detailMaterial;

                }

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        }

    }
}