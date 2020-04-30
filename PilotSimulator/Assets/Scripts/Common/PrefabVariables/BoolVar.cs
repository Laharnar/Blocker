using UnityEngine;
[CreateAssetMenu()]
public class BoolVar : ScriptableObject {
    public bool value;

    public void Set(bool value)
    {
        this.value = value;
    }
}
