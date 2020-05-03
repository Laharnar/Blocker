using System.Collections.Generic;
using UnityEngine;

public class Vec3Array : ScriptableObject
{
    [SerializeField] List<Vec3VarRef> value;
    public bool readOnly = true;
    public List<Vec3VarRef> Value {
        get => value;
        set {
            if (readOnly)
            {
                throw new ReadOnlyException("Vec3Array isn't in write mode.");
            }
            this.value = value;
        }
    }
    public int Count {
        get {
            return value.Count;
        }
    }
}
