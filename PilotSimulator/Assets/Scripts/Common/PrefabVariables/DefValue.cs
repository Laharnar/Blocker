using UnityEngine;

[System.Serializable]
public abstract class DefValue<T> {
    [SerializeField] bool useDefault = true;
    [SerializeField] T defaultValue;

    public T Value {
        get {
            if (useDefault)
                return defaultValue;
            else return GetValue();
        }
        set {
            if (useDefault)
                defaultValue = value;
            else SetValue(value);
        }
    }

    protected abstract void SetValue(T value);
    protected abstract T GetValue();
}
