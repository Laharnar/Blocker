using System;

[System.Serializable]
public class FloatVarRef {
    public FloatVar value;
    public bool useDefault = true;
    public float defaultValue;

    public float Value {
        get {
            if (useDefault)
                return defaultValue;
            else return value.value;
        }
        set {
            if (useDefault)
                defaultValue=value;
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

}

