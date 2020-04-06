using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringListValue : DefValue<List<string>> {
    [SerializeField] StringList value;

    protected override List<string> GetValue()
    {
        return this.value.Value;
    }

    protected override void SetValue(List<string> value)
    {
        this.value.Value = value;
    }
}