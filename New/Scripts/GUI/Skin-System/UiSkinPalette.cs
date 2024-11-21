using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "", menuName = "Skin/Skin Pallete")]
public class UiSkinPalette : ScriptableObject
{

    public string displayName;

    public List<string> skinTypes = new List<string>();

    public ColorPaletteAttribute colorPalette;

    [TableList]
    public List<ComponentSkinDataObject> buttonSkins = new List<ComponentSkinDataObject>();

    [TableList]
    public List<ComponentSkinDataObject> alertSkins = new List<ComponentSkinDataObject>();

    [TableList]
    public List<ComponentSkinDataObject> windowSkins = new List<ComponentSkinDataObject>();

    [TableList]
    public List<ComponentSkinDataObject> textSkins = new List<ComponentSkinDataObject>();

    [TableList]
    public List<ComponentSkinDataObject> toggleSkins = new List<ComponentSkinDataObject>();

    [TableList]
    public List<ComponentSkinDataObject> inputfieldSkins = new List<ComponentSkinDataObject>();

    [TableList]
    public List<ComponentSkinDataObject> miscSkins = new List<ComponentSkinDataObject>();

    [ReadOnly, ShowInInspector]
    public List<ComponentSkinDataObject> skinDataObjects = new List<ComponentSkinDataObject>();

}