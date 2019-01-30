using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{

	public bool[][] layout;
	public Line[] lines;
	public Vector2 startPoint, endPoint;
	public Level()
	{

	}
	public Level(Vector2[] points, float extra) {
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

	}
}
