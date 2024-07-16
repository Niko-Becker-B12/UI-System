using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[Sirenix.OdinInspector.InlineEditor]
[CreateAssetMenu(menuName = "Skin/Component DataObject")]
public class ComponentSkinDataObject : ScriptableObject
{


    [TitleGroup("Background Radius")]
    [HorizontalGroup("Background Radius/Background Radius 1"), LabelText("TL")]
    [Range(0f, 256f)]
    public int backgroundRadiusTL;

    [HorizontalGroup("Background Radius/Background Radius 1"), LabelText("TR")]
    [Range(0f, 256f)]
    public int backgroundRadiusTR;

    [HorizontalGroup("Background Radius/Background Radius 2"), LabelText("BR")]
    [Range(0f, 256f)]
    public int backgroundRadiusBR;

    [HorizontalGroup("Background Radius/Background Radius 2"), LabelText("BL")]
    [Range(0f, 256f)]
    public int backgroundRadiusBL;

    [TitleGroup("Background Radius")]
    public bool useMaxRadius = false;


    [BoxGroup("Background Shadow")]
    public Color shadowColor;

    [BoxGroup("Background Shadow")]
    [HorizontalGroup("Background Shadow/Size")]
    [Range(0f, 256f)]
    public float size = 0f;

    [BoxGroup("Background Shadow")]
    [HorizontalGroup("Background Shadow/Size")]
    [Range(0f, 1f)]
    public float softness = 0f;

    [BoxGroup("Background Shadow")]
    [HorizontalGroup("Background Shadow/Offset")]
    public Vector2 offset;


    [FoldoutGroup("Color")]
    [BoxGroup("Color/Background Color")]
    public ColorBlock backgroundColor;

    [FoldoutGroup("Color")]
    [BoxGroup("Color/Background Color")]
    public Sprite backgroundSprite;

    [BoxGroup("Color/Outline Color")]
    public ColorBlock outlineColor;

    [BoxGroup("Color/Outline Color")]
    [Range(0,100)]
    public int outlineWidth;

    [BoxGroup("Color/Detail Color")]
    public ColorBlock detailColor;



}