using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[Sirenix.OdinInspector.InlineEditor]
[CreateAssetMenu(menuName = "Skin/Toggle DataObject")]
public class UiToggleSkinDataObject : ComponentSkinDataObject
{

    [FoldoutGroup("Color")]
    [BoxGroup("Color/Background Color/Pressed")]
    public ColorBlock pressedBackgroundColor;

    [FoldoutGroup("Color")]
    [BoxGroup("Color/Background Color/Pressed")]
    public Sprite pressedBackgroundSprite;

    [BoxGroup("Color/Outline Color/Pressed")]
    public ColorBlock pressedOutlineColor;

    [BoxGroup("Color/Outline Color/Pressed")]
    [Range(0, 100)]
    public int pressedOutlineWidth;

    [BoxGroup("Color/Detail Color/Pressed")]
    public ColorBlock pressedDetailColor;

}