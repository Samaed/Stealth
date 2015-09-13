using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(MapGeneratorBehaviour), true)]
public class MapGeneratorEditor : Editor
{
    int _choiceIndex;

    public override void OnInspectorGUI()
    {
        var generator = target as MapGeneratorBehaviour;

        MapGenerationPipeline pipeline = generator.GetComponent<MapGenerationPipeline>();

        // Draw the default inspector
        DrawDefaultInspector();

        if (pipeline == null)
        {
            EditorGUILayout.HelpBox("A MapGeneratorPipeline is required", MessageType.Error);
            return;
        }
        else if (!pipeline.enabled)
        {
            EditorGUILayout.HelpBox("The MapGeneratorPipeline is not active", MessageType.Warning);
        }


        string[] _choices = pipeline.Steps.Select(e => e.Name).ToArray();
        _choiceIndex = generator.stepIndex;

        GUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Map Generation Pipeline Step");
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
        GUILayout.EndHorizontal();


        if (_choiceIndex >= 0 && _choiceIndex < _choices.Length)
        {
            // Update the selected choice in the underlying object
            generator.stepIndex = _choiceIndex;
            // Save the changes back to the object*/
            EditorUtility.SetDirty(target);
        }
    }
}