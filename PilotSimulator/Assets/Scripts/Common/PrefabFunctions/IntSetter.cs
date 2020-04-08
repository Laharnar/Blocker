using UnityEngine;

[CreateAssetMenu(menuName = "Setters/IntSetter")]
public class IntSetter : ScriptableObject {
    public IntVarValue from;
    public IntVarValue into;

    public void Activate()
    {
        into.Value = from.Value;
    }
}
