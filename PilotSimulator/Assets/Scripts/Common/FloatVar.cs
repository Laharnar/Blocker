using UnityEngine;

[CreateAssetMenu()]
public class FloatVar : ScriptableObject {
    public float value;
    public float V { get => value; }
}
