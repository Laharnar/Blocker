using UnityEngine;

[System.Serializable]
public class TransformVarValue {
    [SerializeField] TransformRef target;
    [SerializeField] bool useDefault = true;
    [SerializeField] Transform defaultValue;

    public Transform Value {
        get {
            if (useDefault)
            {
                return defaultValue;
            }
            return target.value;
        }
    }
}
