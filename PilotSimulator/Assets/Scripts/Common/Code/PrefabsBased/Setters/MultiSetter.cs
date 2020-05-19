using UnityEngine;

[CreateAssetMenu(menuName = "Setters/MultiSetter")]
public class MultiSetter : ScriptableObject {
    public MultiTypeValue from;
    public MultiTypeValue into;

    public void Activate()
    {
        into.Value = from.Value;
    }
}
