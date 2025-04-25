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

            if (detailGraphic != null)
            {

                detailGraphic.color = skinData.detailColor.normalColor;

                if (detailGraphic is Image)
                {

                    if (skinData.detailSprite != null)
                        (detailGraphic as Image).sprite = skinData.detailSprite;
                    if (skinData.detailMaterial != null)
                        detailGraphic.material = skinData.detailMaterial;

                }

            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        }

    }
}