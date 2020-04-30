using System;
using UnityEngine;

[System.Serializable]
public class Vec3VarRef {
    [SerializeField] Vec3Var value;
    public bool useFloats;
    [SerializeField] FloatVar x;
    [SerializeField] FloatVar y;
    [SerializeField] FloatVar z;

    public bool useDefault = true;
    [SerializeField] Vector3 defaultValue;

    public Vector3 Value {
        get {
            if (useDefault)
                return defaultValue;
            else if (useFloats)
                return new Vector3(
                    x ? x.Value : defaultValue.x, 
                    y ? y.Value : defaultValue.y, 
                    z ? z.Value : defaultValue.z);
            else return value.value;
        }
        set {
            if (useDefault)
                defaultValue = value;
            else if (useFloats)
            {
                x.Value = value.x;
                y.Value = value.y;
                z.Value = value.z;
            }
            else this.value.value = value;
        }
    }

    public string GetPrefabNameOrDefault(string defaultName)
    {
        if (useDefault)
        {
            return defaultName;
        }
        return value.name;
    }

    internal void SetY(float v)
    {
        Vector3 v3 = Value;
        v3.y = v;
        Value = v3;
    }
}

