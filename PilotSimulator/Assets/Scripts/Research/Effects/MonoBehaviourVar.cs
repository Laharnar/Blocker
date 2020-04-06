using System;
using UnityEngine;

[CreateAssetMenu]
public class MonoBehaviourVar:ScriptableObject {
     
    public MonoBehaviour value;
    public ResearchTree ValueAsRt { get => (ResearchTree)Value; }

    public MonoBehaviourTypes expectType;

    public object Value {
        get {
            if (ValueTypeEqualsPrefabType())
                return value;
            return null;
        }
    }

    public T GetValueAs<T>() where T:MonoBehaviour
    {
        if (EqualsPrefabType(typeof(T)))
            return (T)value;
        else throw InvalidIncorrectTypeException(typeof(T));
    }

    private Exception InvalidIncorrectTypeException(Type type)
    {
        return new System.InvalidCastException("Type isn't same as is set in this prefab." + type + " != " + expectType + " n:" + name);
    }

    public void SetValue(MonoBehaviour val)
    {
        if (!val)
        {
            throw new NullReferenceException("setting value to null isn't allowed.");
        }
        this.value = val;
        TrySetCorrectType(val.GetType());
    }

    private bool TrySetCorrectType(Type t)
    {
        if(!Enum.TryParse<MonoBehaviourTypes>(t.ToString(), out expectType))
        {
            throw new NotImplementedException("Type for mono pref doesn't exist in enum "+t);
        }
        return true;
    }

    private bool ValueTypeEqualsPrefabType()
    {
        return EqualsPrefabType(value.GetType());
    }

    private bool EqualsPrefabType(Type t)
    {
        return t.Name == expectType.ToString();
    }

    public enum MonoBehaviourTypes {
        ResearchTree,
        ResearchCluster
    }
}
