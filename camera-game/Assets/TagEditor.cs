using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tag))]
public class TagEditor : Editor
{
    string[] _choices = new[] { "foo", "foobar" };
    int _choiceIndex = 0;

    public override void OnInspectorGUI()
    {
        _choices = UnityEditorInternal.InternalEditorUtility.tags;
        // Draw the default inspector
        DrawDefaultInspector();
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
        var someClass = target as Tag;
        // Update the selected choice in the underlying object
        someClass.choice = _choices[_choiceIndex];
        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}