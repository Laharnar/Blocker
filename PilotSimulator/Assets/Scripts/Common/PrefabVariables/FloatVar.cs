using UnityEngine;

[CreateAssetMenu()]
public class FloatVar : ScriptableObject {
    public float value;
    public float V { get => value; }

    public void SetFloat(float f)
    {
        value = f;
    }
}
