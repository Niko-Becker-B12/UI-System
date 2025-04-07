using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiTooltip : UiElementExtended
{

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