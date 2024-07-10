using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Parabola
{
    public static Vector3 GetParabola(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolaHeight = ParabolaFunc(height, t);

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, parabolaHeight + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    private static float ParabolaFunc(float height, float t)
    {
        return -4 * height * t * t + 4 * height * t;
    }
}
