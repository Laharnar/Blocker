using System;
using UnityEngine;

[System.Serializable]
public class IntVarValue  {
    [SerializeField] IntVar value;
    public bool useDefault = true;
    [SerializeField] int defaultValue;

    public int Value {
        get {
            if (useDefault)
            {
                return defaultValue;
            }
            return value.value;
        }
        set {
            if (useDefault)
            {
                defaultValue = value;
            }
            else
            {
                this.value.value = value;
            }
        }
    }

    internal void SetIntPrefab(IntVar customVal)
    {
        value = customVal;
    }
}
