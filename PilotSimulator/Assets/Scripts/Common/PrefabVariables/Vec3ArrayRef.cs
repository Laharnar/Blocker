using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vec3ArrayRef
{
    [SerializeField] Vec3Array array;

    public bool useDefault = true;
    [SerializeField] List<Vec3VarRef> defVectors;

    public bool readOnly = true;

    public Vector3 this[int index]    // Indexer declaration  
    {
        // get and set accessors 
        get {
            return Value[index].Value;
        }
    }
    public int Count {
        get {
            if (useDefault)
                return defVectors.Count;
            else return array.Count;
        }
    }

    public List<Vec3VarRef> Value {
        get {
            if (useDefault) return defVectors;
            else return array.Value;
        }
        set {
            if (readOnly)
            {
                throw new ReadOnlyException("Vec3Array isn't in write mode.");
            }
            if (useDefault) defVectors = value;
            else array.Value = value;
        }
    }

}