using UnityEngine;

[System.Serializable]
public class Vec3VarRef {
    public Vec3Var value;
    public bool useDefault = true;
    public Vector3 defaultValue;

    public Vector3 Value {
        get {
            if (useDefault)
                return defaultValue;
            else return value.value;
        }
        set {
            if (useDefault)
                defaultValue = value;
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

