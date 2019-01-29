using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor {

	LevelGenerator generator;
	void OnEnable()
    {
		generator = target as LevelGenerator;
		generator.Load();
    }
    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();
        
        if(GUILayout.Button("Generate"))
        {
			generator.Gen();
            SceneView.RepaintAll();
        }
     
   
     
    }
    private void OnSceneGUI()
    {
        drawLines();
    }
    
    void drawLines()
    {
        Handles.color = Color.white;
        generator = target as LevelGenerator;
        for (int i = 0; i < generator.lines.Length; i++)
        {
            Line line = generator.lines[i];
            Handles.DrawLine(new Vector3(line.p0.x, line.p0.y, 0), new Vector3(line.p1.x, line.p1.y, 0));
        }

    }

}
