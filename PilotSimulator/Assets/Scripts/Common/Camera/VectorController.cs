using System;
using UnityEngine;

[Serializable]
public class VectorController
{
    public bool keepX, keepY, keepZ;

    public Vector3 LimitDir(Vector3 v)
    {
        Vector3 dir = v;
        if (keepX)
        {
            dir.x = 0;
        }
        if (keepY)
        {
            dir.y = 0;
        }
        if (keepZ)
        {
            dir.z = 0;
        }
        return dir;
    }
}