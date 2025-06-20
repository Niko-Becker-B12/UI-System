using Sirenix.OdinInspector;
using System;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GPUI
{
    [System.Serializable]
        [Sirenix.OdinInspector.InlineEditor(InlineEditorModes.GUIOnly)]
        [CreateAssetMenu(menuName = "Skin/Component DataObject")]

    public class ComponentSkinDataObject : SimpleComponentSkinDataObject
    {

        [TabGroup("Settings", "Color", SdfIconType.PaintBucket)]
        [TabGroup("Settings/Color/Colors", "Background", SdfIconType.SquareFill)]
        public bool useBackgroundAsMask = false;

        [TabGroup("Settings/Color/Colors", "Outline", SdfIconType.BorderOuter)]
        public ColorBlock outlineColor;

        [TabGroup("Settings/Color/Colors", "Outline")] [Range(0, 100)]
        public int outlineWidth;


        [TabGroup("Settings/Color/Colors", "Detail", SdfIconType.QuestionSquare)]
        public ColorBlock detailColor;

        [TabGroup("Settings/Color/Colors", "Detail")]
        public Sprite detailSprite;

        [TabGroup("Settings/Color/Colors", "Detail")]
        public Material detailMaterial;

    }
}