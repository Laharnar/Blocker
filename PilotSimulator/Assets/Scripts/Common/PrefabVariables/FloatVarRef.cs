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
    }
}

