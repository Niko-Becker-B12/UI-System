#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace GPUI
{
    public class UiDataProcessor : OdinAttributeProcessor<ComponentSkinDataObject>
    {

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            base.ProcessChildMemberAttributes(parentProperty, member, attributes);


            if (member.Name == "abilities")
            {
                var attribute = attributes.GetAttribute<ListDrawerSettingsAttribute>();

                attribute.OnTitleBarGUI =
                    "@UiDataProcessor.CreateNewSkinData($root)"; // could use $value to get Abilities[], or $root to get MyData
            }
        }

        static private void CreateNewSkinData(ComponentSkinDataObject skinData)
        {
            if (SirenixEditorGUI.ToolbarButton(SdfIconType.PlusCircleDotted))
            {
                //ScriptableObjectCreator.ShowDialog<MyData>("Assets\\Resources\\abilities", obj =>
                //{
                //    Array.Resize(ref value.abilities, value.abilities.Length + 1); // If you could, use a List instead
                //    value.abilities[^1] = obj; // Append does not work well
                //});
            }
        }

    }
}
#endif