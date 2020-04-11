using System;
using UnityEngine;

[System.Serializable]
public class MultiTypeValue {
    public bool useInt = false;
    public IntVarValue i;
    [Space]
    public bool useFloat = true;
    public FloatVarRef f;
    [Space]
    public float defaultValue = 0;

    public float Value {
        get {
            if (useInt)
                return i.Value;
            else if (useFloat)
                return f.Value;
            else
            {
                Debug.Log("using default because of missing reference.");
                return defaultValue;
            }
        }
        set {
            if (useInt)
                i.Value = (int)value;
            else if (useFloat)
                f.Value = value;
            else
            {
                Debug.Log("using default because of missing reference.");
                defaultValue = value;
            }
        }
    }

    public string Type { get => useInt ? "int" : useFloat ? "float": "undefined"; }

    public string GetNameOrDefault(string def)
    {
        if (useInt)
        {
            return i.GetPrefabNameOrDefault(def);
        }
        else if (useFloat)
        {
            return f.GetPrefabNameOrDefault(def);
        }
        else return def;
    }
}
