using System;
using UnityEngine;

[Serializable]
public class RectVarValue
{

    [SerializeField] RectVar value;

    public bool useDefault = true;
    [SerializeField] Rect defaultValue;

    public Rect Value
    {
        get
        {
            if (useDefault)
            {
                return defaultValue;
            }
            return value.value;
        }
        set
        {
            if (useDefault)
            {
                defaultValue = value;
            }
            else
            {
                this.value.value = value;
            }
        }
    }

    public string GetPrefabNameOrDefault(string defaultName)
    {
        if (useDefault)
        {
            return defaultName;
        }
        return value.name;
    }

    internal void SetRectPrefab(RectVar customVal)
    {
        value = customVal;
    }
}
