using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public int pointCount;
	public int range;
	public int extra = 1;
	public Line[] lines = { };
	GameObject tile, point, endTile;
	public int resolution = 1000;
	public int linewidth = 5;
	public bool[,] layout;
	public Vector2 endPoint;
	public Vector2[] points;
	public int levelCount;
	public Level[] levels;

	public void GenPoints()
	{
		points = new Vector2[pointCount];
		points[0] = new Vector2(10, 10);
		for (int i = 1; i < pointCount; i++)
		{
			points[i] = new Vector2(Random.Range(0, range), Random.Range(0, range));

		}
		endPoint = points[pointCount - 1];
	}
	public void GenLines()
	{
		//lines = new Level(points).lines;
		lines = new Line[pointCount - 1 + extra];

		for (int i = 0; i < pointCount - 1; i++)
		{
			lines[i] = new Line(points[i], points[i + 1]);
		}
		for (int i = 0; i < extra; i++)
		{
			lines[pointCount - 1 + i] = new Line(points[Random.Range(0, pointCount - 1)], points[Random.Range(0, pointCount - 1)]);
		}
	}
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

		GenPoints();
		for (int p = 0; p < points.Length; p++)
		{
			Instantiate(point, new Vector3(points[p].x, points[p].y, 0), Quaternion.identity, tiles.transform);
		}
		GenLines();

		layout = new bool[range * resolution, range * resolution];

		for (int l = 0; l < lines.Length; l++)
		{
			Line line = lines[l];
			float d = line.distance,
			slope = line.slope;
			Vector2 pslope = line.difference.normalized;
			for (float step = 0; step <= d * resolution; step += 0.1f)
			{
				float a = d == 0 ? 0.0f : step / d;
				Vector2 p = line.GetPoint(a);
				layout[(int)(p.x * resolution), (int)(p.y * resolution)] = true;

				for (int i = -linewidth; i < linewidth; i++)
				{
					//negative inverse of slope
					Vector2 v = PointOnSlope(p, i * 0.01f, -1 / slope);
					if (v.x > 0 && v.x < range && v.y > 0 && v.y < range)
					{
						layout[(int)(v.x * resolution), (int)(v.y * resolution)] = true;
					}
					else
					{
						Debug.Log(v);
					}


				}
			}
		}
		Instantiate(endTile, new Vector3(endPoint.x, endPoint.y, 0), Quaternion.identity, tiles.transform);
		for (int x = 0; x < range * resolution; x++)
		{
			for (int y = 0; y < range * resolution; y++)
			{
				if (layout[x, y])
				{
					Instantiate(tile, new Vector3((float)x / (float)resolution, (float)y / (float)resolution, 0), Quaternion.identity, tiles.transform);
				}
			}

		}
	}
	void OnAwake()
	{
		Load();
		Gen();

	}
	Vector2 PointOnSlope(Vector2 start, float distance, float slope)
	{
		Vector2 a, b;
		if (slope == 0)
		{
			a.x = start.x + distance;
			a.y = start.y;

			b.x = start.x - distance;
			b.y = start.y;
		}

		else if (slope == Mathf.Infinity)
		{
			a.x = start.x;
			a.y = start.y + distance;

			b.x = start.x;
			b.y = start.y - distance;

		}
		else
		{
			float dx = (distance / Mathf.Sqrt(1 + (slope * slope)));
			float dy = slope * dx;
			a.x = start.x + dx;
			a.y = start.y + dy;

			b.x = start.x - dx;
			b.y = start.y - dy;
		}
		if (distance > 0)
		{
			return a;
		}
		else
		{
			return b;
		}

	}
	private float distance(Vector2 a, Vector2 b)
	{
		return Mathf.Sqrt(((a.x - b.x) * (a.x - b.x)) + ((a.y - b.y) * (a.y - b.y)));
	}
	public bool[,] GetLayout()
	{
		Debug.Log(layout);
		return layout;
	}
	public void Load()
	{
		tile = Resources.Load<GameObject>("tile");
		point = Resources.Load<GameObject>("Point");
		endTile = Resources.Load<GameObject>("End");
	}
}