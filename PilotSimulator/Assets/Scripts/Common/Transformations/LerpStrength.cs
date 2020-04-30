using System;
using UnityEngine;

[Serializable]
public class LerpStrength
{
    public BoolVarValue useLerp;
    public Vec3VarRef a, b;

    [Range(0.0f, 1.0f)]
    public float lerpStrength = 1;

    public Vector3 ApplyIfUsed(Vector3 from, Vector3 to)
    {
        if (useLerp.Value)
            return Vector3.Lerp(from, to, lerpStrength);
        else return from;
    }

    public Vector3 ApplyIfUsed()
    {
        if (useLerp.Value) 
            return Vector3.Lerp(a.Value, b.Value, lerpStrength);
        else return a.Value;
    }
}
