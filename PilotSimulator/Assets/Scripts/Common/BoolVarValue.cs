using UnityEngine;

[System.Serializable]
public class BoolVarValue {
    [SerializeField] BoolVar value;
    public bool useDefault = true;
    public bool defaultValue;

    public bool Value {
        get {
            if (useDefault)
                return defaultValue;
            else return value.value;
        }
    }
}
