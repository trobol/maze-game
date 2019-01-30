using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{

	public bool[,] layout;
	public Line[] lines;
	public Vector2 startPoint, endPoint;
	public Level()
	{

	}
	public Level(Vector2[] points, float extra, int resolution, int range)
	{
		lines = new Line[points.Length];
		int start = 0,
		end;
		for (int i = 0; i < points.Length - 1; i++)
		{
			lines[i] = new Line(points[i], points[i + 1]);
		}
		for (int i = 0; i < extra; i++)
		{
			lines[points.Length - 1 + i] = new Line(points[Random.Range(0, points.Length - 1)], points[Random.Range(0, points.Length - 1)]);
		}

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
				/* 
								for (int i = -linewidth; i < linewidth; i++)
								{
									//negative inverse of slope
									Vector2 v = PointOnSlope(p, i * 0.01f, -1 / slope);
									if (v.x > 0 && v.x < range && v.y > 0 && v.y < range)
									{
										layout[(int)(v.x * resolution), (int)(v.y * resolution)] = 1;
									}
									else
									{
										Debug.Log(v);
									}
									}
*/


			}
		}

	}
}
