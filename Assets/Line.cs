using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Line
{
	public Vector2 p0, p1;
	public Line(Vector2 i0, Vector2 i1)
	{
		p0 = i0;
		p1 = i1;
		CalcValues();
	}

	public Line()
	{
		p0 = Vector2.zero;
		p1 = Vector2.zero;
		CalcValues();
	}
	private void CalcValues()
	{

		Vector2 v = p0 - p1;
		slope = v.y / v.x;
		distance = Mathf.Sqrt(((p1.x - p0.x) * (p1.x - p0.x)) + ((p1.y - p0.y) * (p1.y - p0.y)));
	}
	public Vector2 GetPoint(float a)
	{
		return Vector2.Lerp(p0, p1, a);
	}
	public Vector2 GetPointAt(float a, Vector2 v)
	{
		if (v.x != 0)
		{
			if (a < smaller.x || a > larger.x)
				throw null;
			return GetPoint(Mathf.Abs(a - smaller.x) / difference.x);
		}
		else if (v.y != 0)
		{
			if (a < smaller.y || a > larger.y)
				throw null;
			return GetPoint(Mathf.Abs(a - smaller.y) / difference.y);
		}
		else
		{
			throw null;
		}

	}
	public float slope, magnitude, distance;

	public Vector2 larger
	{
		get { return p0.magnitude > p1.magnitude ? p0 : p1; }
	}
	public Vector2 smaller
	{
		get { return p0.magnitude < p1.magnitude ? p0 : p1; }
	}


	public Vector2 difference
	{
		get { return p1 - p0; }

	}
	public Vector2 normalized
	{
		get { return difference.normalized; }
	}
	public static Vector2[] DrawLine(Vector2 a, Vector2 b) {
		//Vector2[] c;
	}
}
