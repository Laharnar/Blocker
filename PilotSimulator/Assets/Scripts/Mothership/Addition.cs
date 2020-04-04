using UnityEngine;

[CreateAssetMenu(menuName = "Operators/Addition")]
public class Addition:ScriptableObject {

    public IntVarValue a;
    public IntVarValue b;
    public IntVarValue result;

    // For event system.
    public void Add()
    {
        result.Value = a.Value + b.Value;
    }
}
