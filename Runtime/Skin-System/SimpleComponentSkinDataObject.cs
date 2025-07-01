using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GPUI
{
    [System.Serializable]
        [Sirenix.OdinInspector.InlineEditor(InlineEditorModes.GUIOnly)]
        [CreateAssetMenu(menuName = "Skin/Simple Component DataObject")]

    public class SimpleComponentSkinDataObject : ScriptableObject
    {

        [InfoBox("$InfoBoxMessage", InfoMessageType.Error, "HasIssues")]
        [PropertyOrder(-1), BoxGroup("Name"), TableColumnWidth(200, resizable: false), HideLabel]
        [ShowInInspector, ReadOnly]
        public string Name
        {
            get { return name.Replace("SKN_UI_", ""); }
        }
        [PropertyOrder(-1), BoxGroup("Name"), TableColumnWidth(200, resizable: false), HideLabel]
        [ShowInInspector, ReadOnly, InlineEditor(InlineEditorModes.SmallPreview)]
        public Object target => this;

        [TabGroup("Settings", "Shape", SdfIconType.BoundingBoxCircles)]
        [HorizontalGroup("Settings/Shape/Left"), LabelText("Top-Left")]
        [Range(0f, 256f)]
        public int backgroundRadiusTL;

        [TabGroup("Settings", "Shape")]
        [HorizontalGroup("Settings/Shape/Right"), LabelText("Top-Right")]
        [Range(0f, 256f)]
        public int backgroundRadiusTR;

        [TabGroup("Settings", "Shape")]
        [HorizontalGroup("Settings/Shape/Left"), LabelText("Bottom-Left")]
        [Range(0f, 256f)]
        public int backgroundRadiusBL;

        [TabGroup("Settings", "Shape")]
        [HorizontalGroup("Settings/Shape/Right"), LabelText("Bottom-Right"), InlineProperty]
        [Range(0f, 256f)]
        public int backgroundRadiusBR;

        [TabGroup("Settings", "Shape")] public bool useMaxRadius = false;

        [TabGroup("Settings", "Shape")] [Range(1, 10)]
        public int shapeRoundness = 10;

        [TabGroup("Settings", "Layout", SdfIconType.ColumnsGap)]
        public bool useLayoutOptions = false;
        
        [System.Serializable]
        public struct UiElementLayoutOptions
        {
            
            [System.Flags]
            public enum UiElementSizingOptions
            {

                ContentSizeFitted = 1,
                ParentSizeFixed = 2,
                ControlChildHeight = 4 | 2,
                ControlChildWidth = 8 | 2

            }
            
            public TextAnchor childAlignment;
            
            public UiElementSizingOptions layoutSizingOption;

            public enum UiElementChildAlignmentAxis
            {
                None,
                Vertical,
                Horizontal
            }


            public UiElementChildAlignmentAxis childAlignmentAxis;

  
            public RectOffset layoutMargin;

            [Range(0f, 256f)]
            public float layoutSpacing;
            
            public bool reverseLayout;
            
        }

        [TabGroup("Settings", "Layout"), ShowIf("useLayoutOptions")]
        public UiElementLayoutOptions layoutOptions;

        [TabGroup("Settings", "Shadow", SdfIconType.Subtract)]
        public Color shadowColor;


        [TabGroup("Settings", "Shadow")] [Range(0f, 2048f)]
        public float size = 0f;

        [TabGroup("Settings", "Shadow")] [Range(0f, 1f)]
        public float softness = 0f;

        [TabGroup("Settings", "Shadow")] public Vector2 offset;


        [TabGroup("Settings", "Color", SdfIconType.PaintBucket)]
        [TabGroup("Settings/Color/Colors", "Background", SdfIconType.SquareFill)]
        public ColorBlock backgroundColor;

        [TabGroup("Settings/Color/Colors", "Background")]
        public Sprite backgroundSprite;

        [TabGroup("Settings/Color/Colors", "Background")]
        public Material backgroundMaterial;
        
        [TabGroup("Settings", "Color", SdfIconType.PaintBucket)]
        [TabGroup("Settings/Color/Colors", "Background", SdfIconType.SquareFill)]
        public bool useBackgroundAsMask = false;

        [TabGroup("Settings/Color/Colors", "Outline", SdfIconType.BorderOuter)]
        public ColorBlock outlineColor;

        [TabGroup("Settings/Color/Colors", "Outline")] [Range(0, 100)]
        public int outlineWidth;

        private string infoBoxMessage;

        public string InfoBoxMessage
        {

            get
            {

                if (!name.Contains("SKN_UI_"))
                    infoBoxMessage = "Name is invalid! Name does not contain correct Prefix-Syntax!";
                else if (name.IsNullOrWhitespace())
                    infoBoxMessage = "Name is invalid!";
                else if (name.Contains(" "))
                    infoBoxMessage = "Name is invalid! Make sure the name does not contain any spaces!";
                else
                {
                    infoBoxMessage = "";
                }

                return infoBoxMessage;

            }

        }

        public bool HasIssues()
        {


            return !InfoBoxMessage.IsNullOrWhitespace();

        }

    }
}