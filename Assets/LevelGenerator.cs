using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	public int height, width;
	public int lineCount;
	public Vector2 range;
	public int extra = 1;
	public Line[] lines = { };
	GameObject tile;

	public float tilesize = 1;
	public void Gen()
	{
		Transform t = transform.Find("Tiles");
		if (t != null)
		{
			DestroyImmediate(t.gameObject);
		}

		GameObject tiles = new GameObject("Tiles");
		tiles.transform.parent = gameObject.transform;
		Debug.Log("Generate");

		lines = new Line[lineCount + extra];
		Vector2 start = new Vector2(0, 0),
		p0 = new Vector2(Random.Range(range.x, range.y), Random.Range(range.x, range.y)),
		p1;

		for (int i = 0; i < lineCount; i++)
		{
			p1 = new Vector2(p0.x + Random.Range(range.x, range.y), p0.y + Random.Range(range.x, range.y));
			lines[i] = new Line(p0, p1);
			p0 = p1;
		}
		for (int i = 0; i < extra; i++)
		{
			p0 = lines[Random.Range(0, lineCount - 1)].p0;
			p1 = lines[Random.Range(0, lineCount - 1)].p0;
			lines[lineCount + i] = new Line(p0, p1);
		}

		for (int l = 0; l < lines.Length; l++)
		{
			Line line = lines[l];
			Vector2 larger, smaller, dif, tilePos;

			if (line.p0.x > line.p1.x)
			{
				larger = line.p0;
				smaller = line.p1;
			}
			else
			{
				larger = line.p1;
				smaller = line.p0;
			}
			dif = larger - smaller;
			for (int x = 0; x < (int)dif.x; x++)
			{
				tilePos = line.GetPoint(x / dif.x);
				Instantiate(tile, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity, tiles.transform);
			}
		}

	}
	public void Load()
	{
		tile = Resources.Load<GameObject>("tile");
	}

}