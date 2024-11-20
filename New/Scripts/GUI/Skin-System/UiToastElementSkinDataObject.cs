using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[Sirenix.OdinInspector.InlineEditor]
[CreateAssetMenu(menuName = "Skin/Toast SkinDataObject")]
public class UiToastElementSkinDataObject : ComponentSkinDataObject
{

    [FoldoutGroup("Color")]
    public Sprite toastIcon;
    
    public ComponentSkinDataObject closeButtonSkinData;
    public ComponentSkinDataObject additionalButtonSkinData;

}
