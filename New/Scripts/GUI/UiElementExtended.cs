using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UiElementExtended : UiElement
{

    [FoldoutGroup("UI Elements")]
    public Graphic detailGraphic;

    public override void ApplySkinData()
    {

        base.ApplySkinData();

        if (skinData == null)
            return;

        if (detailGraphic != null)
        {

            detailGraphic.color = skinData.detailColor.normalColor;

        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundGraphic.rectTransform);

    }

}
