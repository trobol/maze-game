using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor {

	LevelGenerator generator;
	void OnEnable()
    {
		generator = (target as LevelGenerator);
		generator.Load();
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if(GUILayout.Button("Generate"))
        {
			generator.Gen();
        }
    }

}
