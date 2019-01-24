using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public int height, width;
    public int lineCount;
    public Vector2 range;
    public Line[] lines = { };
	GameObject tile;
    public void Gen() {
        Transform t = transform.Find("Tiles");
        if (t != null) {
            DestroyImmediate(t.gameObject);
        }
        /*
        GameObject tiles = new GameObject("Tiles");
        tiles.transform.parent = gameObject.transform;
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity, tiles.transform);
            }
        }
        Debug.Log("Generate");
        */
        lines = new Line[lineCount];
        Vector2 p0 = new Vector2(Random.Range(range.x, range.y), Random.Range(range.x, range.y)), p1;
        for (int i = 0; i < lineCount; i++)
        {
            p1 = new Vector2(Random.Range(range.x, range.y), Random.Range(range.x, range.y));
            lines[i] = new Line(p0, p1);
            p0 = p1;
        }
        print(lines);
	}
	public void Load() {
		tile = Resources.Load<GameObject>("tile");
	}
}