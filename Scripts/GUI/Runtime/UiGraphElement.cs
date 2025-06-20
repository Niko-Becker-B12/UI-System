using System.Collections.Generic;
using ThisOtherThing.UI.Shapes;
using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;

namespace GPUI
{
    public class UiGraphElement : UiElement
    {

        public List<float> values = new List<float>();



        public override void ApplySkinData()
        {
            base.ApplySkinData();

            rectTransform = GetComponent<RectTransform>();

            if (backgroundGraphic != null)
            {

                if (backgroundGraphic is Line)
                {

                    Line line = backgroundGraphic as Line;

                    line.ShapeProperties.FillColor = this.skinData.backgroundColor.normalColor;
                    line.PointListsProperties.PointListProperties = new PointsList.PointListProperties[1];

                    Debug.Log(
                        $"{line.PointListsProperties.PointListProperties.Length} {line.PointListsProperties.PointListProperties[0]?.GeneratorData}");

                    line.PointListsProperties.PointListProperties[0].GeneratorData =
                        new PointsList.PointListGeneratorData()
                        {

                            Generator = PointsList.PointListGeneratorData.Generators.LineGraph,
                            Width = rectTransform.sizeDelta.x,
                            Height = rectTransform.sizeDelta.y,
                            Radius = rectTransform.sizeDelta.x / 2,
                            FloatValues = values.ToArray()

                        };

                }

            }

        }
    }
}