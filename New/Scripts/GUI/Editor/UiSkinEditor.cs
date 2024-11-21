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
            Height = 48,
            IconPadding = 0f,
            BorderAlpha = 0.323f
        };
        tree.DefaultMenuStyle = customMenuStyle;
        tree.Config.DrawSearchToolbar = true;

        CreateNewPaletteData newPaletteData = new CreateNewPaletteData();
        
        
        tree.Add("Create New Pallete", newPaletteData);

        var s = tree.AddAllAssetsAtPath("Palletes", "Content/GUI/Styles/",
            typeof(UiSkinPalette), true, true);

        foreach(var t in tree.MenuItems[1].ChildMenuItems)
        {

            tree.AddAllAssetsAtPath($"Palletes/{t.Name}", $"Content/GUI/Styles/{(t.Value as UiSkinPalette).displayName}", typeof(ComponentSkinDataObject), true);

        }

        tree.AddAllAssetsAtPath("All Skin Data Objects", "Content/GUI/Styles/",
            typeof(ComponentSkinDataObject), true, false);


        tree.EnumerateTree()
            .AddThumbnailIcons()
            .SortMenuItemsByName();

        return tree;
    }

    private class PalleteMenuItem : OdinMenuItem
    {
        private readonly UiSkinPalette instance;

        public PalleteMenuItem(OdinMenuTree tree, UiSkinPalette instance) : base(tree, instance.displayName, instance)
        {
            this.instance = instance;
        }

        protected override void OnDrawMenuItem(Rect rect, Rect labelRect)
        {

        }

        public override string SmartName { get { return this.instance.displayName; } }
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
            
            if(!Directory.Exists($"Assets/Content/GUI/Styles/{paletteData.displayName}"))
                Directory.CreateDirectory($"Assets/Content/GUI/Styles/{paletteData.displayName}");
            
            AssetDatabase.CreateAsset(paletteData, $"Assets/Content/GUI/Styles/{paletteData.displayName}/{paletteData.name}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
        }

        [Button]
        public void CreateBaseComponentSkins()
        {
            
            SavePalette();
            
            
            
        }

    }

}

#endif