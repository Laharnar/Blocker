using System;
using UnityEngine;

[Serializable]
public class RandomInArea
{
    public BoolVarValue useRandom;
    public RectVarValue randomAreaAround;
    [SerializeField] Vec3VarRef selectedPoint;
    public Vector3 Value { get => selectedPoint.Value; }
    public Vector3 NextRandom()
    {
        if (useRandom.Value)
        {
            selectedPoint.Value = NewRandom(randomAreaAround.Value);
        }
        return selectedPoint.Value;
    }

    private Vector2 NewRandom(Rect rect)
    {
        return new Vector2(
            randomAreaAround.Value.x + UnityEngine.Random.value * randomAreaAround.Value.width,
            randomAreaAround.Value.y + UnityEngine.Random.value * randomAreaAround.Value.height);
    }

    public void AsGizmo(Vector3 src)
    {
        Gizmos.DrawWireCube(src, selectedPoint.Value);
    }
}
