#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Sirenix.Utilities;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using System.IO;
using Sirenix.OdinInspector;

public class UiSkinEditor : OdinMenuEditorWindow
{

    [MenuItem("Window/UI Editor")]
    private static void OpenWindow()
    {
        var window = GetWindow<UiSkinEditor>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
    }

    protected override void OnBeginDrawEditors()
    {

        OdinMenuTreeSelection selection = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {

            if (SirenixEditorGUI.ToolbarButton("Delete Current"))
            {

                Object asset = selection.SelectedValue as Object;
                string path = AssetDatabase.GetAssetPath(asset);

                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();

            }

            GUILayout.FlexibleSpace();

        }
        SirenixEditorGUI.EndHorizontalToolbar();

    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);

        var customMenuStyle = new OdinMenuStyle
        {
            BorderPadding = 0f,
            AlignTriangleLeft = true,
            TriangleSize = 16f,
            TrianglePadding = 0f,
            Offset = 20f,
            Height = 36,
            IconPadding = 0f,
            BorderAlpha = 0.323f
        };
        tree.DefaultMenuStyle = customMenuStyle;
        tree.Config.DrawSearchToolbar = true;


        tree.Add("Create New Pallete", new CreateNewPaletteData());

        tree.Add("Palettes", new DisplayAllPalettes());

        tree.AddAllAssetsAtPath("Palettes", $"{UiSettings.instance.PalettePath.Replace("Assets/", "")}/",
            typeof(UiSkinPalette), true, true);

        foreach(var t in tree.MenuItems[1].ChildMenuItems)
        {

            tree.AddAllAssetsAtPath($"Palettes/{t.Name}", $"{UiSettings.instance.PalettePath.Replace("Assets/", "")}/{(t.Value as UiSkinPalette).displayName}", typeof(ComponentSkinDataObject), true);

        }

        tree.AddAllAssetsAtPath("All Skin Data Objects", $"{UiSettings.instance.PalettePath.Replace("Assets/", "")}/",
            typeof(ComponentSkinDataObject), true, false);


        tree.EnumerateTree()
            .AddThumbnailIcons()
            .SortMenuItemsByName();

        return tree;
    }

    private class CreateNewPaletteData
    {

        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public UiSkinPalette paletteData;

        public CreateNewPaletteData()
        {

            paletteData = ScriptableObject.CreateInstance<UiSkinPalette>();
            paletteData.name = "SKN_UI_Palette_";

        }

        [Button]
        public void SavePalette()
        {

            paletteData.name = $"SKN_UI_Palette_{paletteData.displayName}";

            if (!Directory.Exists($"{UiSettings.instance.PalettePath}/{paletteData.displayName}"))
                Directory.CreateDirectory($"Assets/Content/GUI/Styles/{paletteData.displayName}");

            AssetDatabase.CreateAsset(paletteData, $"{UiSettings.instance.PalettePath}/{paletteData.displayName}/{paletteData.name}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

        [Button]
        public void CreateBaseComponentSkins()
        {

            SavePalette();



        }

    }

    private class DisplayAllPalettes
    {

        [TableList]
        public List<UiSkinPalette> allPalettes = new List<UiSkinPalette>();

        public DisplayAllPalettes()
        {

            allPalettes = AssetDatabase
            .FindAssets($"t:{typeof(UiSkinPalette).Name}")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<UiSkinPalette>).ToList();

        }

    }

}

#endif