using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(MapGenerationPipeline))]
public class MapGenerationPipelineEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("Steps"),
                true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Steps");
        };

        list.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.LabelField(
                new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight),
                new GUIContent("Step"));
            EditorGUI.PropertyField(
                new Rect(rect.x + 50, rect.y, rect.width - 50 - 20, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Name"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width - 15, rect.y, 20, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Active"), GUIContent.none);
        };

        list.onAddCallback = (ReorderableList l) =>
        {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("Name").stringValue = "Step"+index;
            element.FindPropertyRelative("Active").boolValue = true;
        };

        list.onCanRemoveCallback = (ReorderableList l) =>
        {
            return l.count > 1;
        };
    }

    public override void OnInspectorGUI()
    {
        var pipeline = target as MapGenerationPipeline;
        if (pipeline.GetComponent<Map>() == null)
        {
            EditorGUILayout.HelpBox("A Map Component is required", MessageType.Error);
            return;
        }

        DrawDefaultInspector();

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

}
