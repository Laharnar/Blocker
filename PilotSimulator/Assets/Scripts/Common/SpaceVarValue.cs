using UnityEngine;

[System.Serializable]
public class SpaceVarValue {
    public SpaceVar value;
    public bool useDefault = true;
    public Space defaultValue;

    public Space Value {
        get {
            if (useDefault)
                return defaultValue;
            else return value.value;
        }
    }
}
