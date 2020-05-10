using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Transform Direction")]
public class TransformDirection:SimpleEffect
{
    [SerializeField] private Vec3VarRef change;
    [SerializeField] private TransformVarValue target;
    [SerializeField] private Vec3VarRef result;

    [SerializeField] private bool applyTransformDirection;
    [SerializeField] private bool applyInverseTransformDirection;

    protected override void DoEffect()
    {
        result.Value = TransformVector(target.Value, change.Value);
    }

    Vector3 TransformVector(Transform transform, Vector3 inp)
    {
        if (applyTransformDirection)
            return transform.TransformDirection(inp);
        if (applyInverseTransformDirection)
            return transform.InverseTransformDirection(inp);
        return inp;
    }
}
