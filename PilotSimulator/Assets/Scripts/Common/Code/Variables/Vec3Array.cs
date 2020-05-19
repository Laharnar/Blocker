using System.Collections.Generic;
using UnityEngine;

public class Vec3Array : ScriptableObject
{
    [SerializeField] List<Vec3VarRef> value;
    public bool readOnly = true;

    private int tmpId;

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
    public void InsertId(int id)
    {
        this.tmpId = id;
    }

    public void InsertDo(Vector3 target)
    {
        Value.Insert(tmpId, new Vec3VarRef() { useDefault = true, useFloats = false, Value = target });
    }

    public void Add(Vector3 target)
    {
        Value.Add(new Vec3VarRef() { useDefault = true, useFloats = false, Value = target });
    }
}
