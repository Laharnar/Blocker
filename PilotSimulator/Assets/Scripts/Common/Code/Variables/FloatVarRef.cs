using System;
using UnityEngine;

[System.Serializable]
public class FloatVarRef {
    public FloatVar value;
    public bool useDefault = true;
    public float defaultValue;
    [Space]
    public float log;
    public float Value {
        get {
            if (useDefault)
                return log = defaultValue;
            else return log = value.Value;
        }
        set {
            if (useDefault)
                defaultValue = value;
            else
            {
                this.value.Value = ( value);
            }
            log = value;
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

}

