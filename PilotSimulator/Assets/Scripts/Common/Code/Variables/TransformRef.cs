using UnityEngine;

[CreateAssetMenu]
public class TransformRef : ScriptableObject {
    public Transform value;

    public void SetTransform(TransformRef other)
    {
        value = other.value;
    }

    public void SetTransform(Transform other)
    {
        value = other;
    }
}
