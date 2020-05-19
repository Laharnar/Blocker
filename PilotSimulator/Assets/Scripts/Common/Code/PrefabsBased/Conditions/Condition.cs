using UnityEngine;

public abstract class Condition:ScriptableObject {
    public abstract bool IsTrue();

    public bool IsFalse()
    {
        return !IsTrue();
    }
}
