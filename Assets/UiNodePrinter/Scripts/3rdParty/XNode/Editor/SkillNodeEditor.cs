using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace CleverCrow.UiNodeBuilder.ThirdParty.XNodes.Editors {
    [CustomNodeEditor(typeof(SkillNode))]
    public class SkillNodeEditor : NodeEditor {
        public override void OnBodyGUI () {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("enter"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("_displayName"));

            var image = serializedObject.FindProperty("_graphic").objectReferenceValue as Sprite;
            GUILayout.Box(image.texture, GUILayout.Width(50), GUILayout.Height(50));

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));
        }
    }
}