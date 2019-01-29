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
    }
    public Line()
    {
        p0 = Vector2.zero;
        p1 = Vector2.zero;
    }
}
