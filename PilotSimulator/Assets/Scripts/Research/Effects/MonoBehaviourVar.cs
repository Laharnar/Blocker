using System;
using UnityEngine;

[CreateAssetMenu]
public class MonoBehaviourVar:ScriptableObject {
     
    public MonoBehaviour value;
    public ResearchTree ValueAsRt { get => (ResearchTree)Value; }

    public MonoBehaviourTypes expectType;

    public object Value {
        get {
            if (IsCorrectType())
                return value;
            return null;
        }
    }

    internal void SetValue(MonoBehaviour val)
    {
        if (!val)
        {
            throw new NullReferenceException("setting value to null isn't allowed.");
        }
        this.value = val;
        TrySetCorrectType(val.GetType());
    }

    public bool TrySetCorrectType(Type t)
    {
        if(!Enum.TryParse<MonoBehaviourTypes>(t.ToString(), out expectType))
        {
            throw new NotImplementedException("Type for mono pref doesn't exist in enum "+t);
        }
        return true;
    }

    public bool IsCorrectType()
    {
        return value.GetType().Name == expectType.ToString();
    }

    public enum MonoBehaviourTypes {
        ResearchTree,
        ResearchCluster
    }
}
