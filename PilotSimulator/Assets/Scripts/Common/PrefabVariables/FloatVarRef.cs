using System;
using UnityEngine;

[System.Serializable]
public class FloatVarRef {
    public FloatVar value;
    public bool useDefault = true;
    public float defaultValue;
    public bool log = false;
    public float Value {
        get {
            if (useDefault)
                return defaultValue;
            else return value.Value;
        }
        set {
            if (useDefault)
                defaultValue = value;
            else
            {
                if(log)Debug.Log("fvr: "+this.value.Value+" = "+ value);

                this.value.Value = ( value);
            }
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

