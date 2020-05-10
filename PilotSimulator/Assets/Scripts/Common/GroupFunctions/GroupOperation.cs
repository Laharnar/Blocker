using System;
using UnityEngine;
[System.Serializable]
public class GroupOperation
{
    public GroupOperations op;
    public EmptyValues emptyInput;
    
    public Vector3 Operate(Vector3[] vector3s)
    {
        if(vector3s.Length == 0)
        {
            return emptyInput.vector;
        }
        switch (op)
        {
            case GroupOperations.TakeFirst:
                return First(vector3s);
            case GroupOperations.TakeLast:
                return Last(vector3s);
            case GroupOperations.TakeAverage:
                return Avg(vector3s);
            case GroupOperations.TakeMin:
                return MinManhattan(vector3s);
            case GroupOperations.TakeMax:
                throw new NotImplementedException("GroupOperations Not implemented as max manhatt" + op);
            case GroupOperations.TakeMultipliedWeight:
                throw new NotImplementedException("GroupOperations Not implemented as [i]/max" + op);
            default:
                throw new NotSupportedException("Operation in GroupOperation isn't handled " + op);
        }
    }
    private static Vector3 LeftBottomBounds(Vector3[] vector3s)
    {
        // Vector with smallest sum of xyz
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        for (int i = 0; i < vector3s.Length; i++)
        {
            if (vector3s[i].x < min.x)
                min.x = vector3s[i].x;
            if (vector3s[i].y < min.y)
                min.y = vector3s[i].y;
            if (vector3s[i].z < min.z)
                min.z = vector3s[i].z;
        }
        return min;
    }

    private static Vector3 MinManhattan(Vector3[] vector3s)
    {
        // Vector with smallest sum of xyz
        Vector3 best = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        float minManthatt = float.MaxValue;
        float tmp = 0;
        for (int i = 0; i < vector3s.Length; i++)
        {
            tmp = vector3s[i].x + vector3s[i].y + vector3s[i].z;
            if (minManthatt > tmp)
            {
                minManthatt = tmp;
                best = vector3s[i];
            }
        }
        return best;
    }

    private static Vector3 First(Vector3[] vector3s)
    {
        return vector3s[0];
    }

    private static Vector3 Last(Vector3[] vector3s)
    {
        return vector3s[vector3s.Length - 1];
    }

    private static Vector3 Avg(Vector3[] vector3s)
    {
        return Sum(vector3s) / vector3s.Length;
    }

    private static Vector3 Sum(Vector3[] vector3s)
    {
        Vector3 sum = Vector3.zero;
        for (int i = 0; i < vector3s.Length; i++)
        {
            sum += vector3s[i];
        }
        return sum;
    }
    public enum GroupOperations
    {
        TakeFirst,
        TakeLast,
        TakeAverage,
        TakeMin,
        TakeMax,
        TakeMultipliedWeight
    }
}
