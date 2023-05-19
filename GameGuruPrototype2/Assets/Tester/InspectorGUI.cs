using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(PlatformSpawner))]
public class ItemLevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //PlatformSpawner generator = (PlatformSpawner)target;

        /*if (GUILayout.Button("Generate"))
        {
            generator.Create();
        }
        if (GUILayout.Button("Clear"))
        {
            generator.Clear();
        }*/

    }
}
