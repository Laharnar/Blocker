using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        set {
            Value[index].Value = value;
        }
    }

    internal void RemoveAt(int i)
    {
        if (useDefault)
        {
            defVectors.RemoveAt(i);
        }
        else
        {
            array.Value.RemoveAt(i);
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

    public Vector3[] VectorsCopy() {
        Vector3[] vectors = new Vector3[Value.Count];
        if (useDefault)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = defVectors[i].Value;
            }
        }
        else
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = array.Value[i].Value;
            }
        }
        return vectors;
    }
        

    internal void Insert(int id, Vector3 target)
    {
        Value.Insert(id, new Vec3VarRef() { useDefault = true, useFloats = false, Value = target });
    }

    internal void Add(Vector3 target)
    {
        Value.Add(new Vec3VarRef() { useDefault = true, useFloats = false, Value = target });
    }
}