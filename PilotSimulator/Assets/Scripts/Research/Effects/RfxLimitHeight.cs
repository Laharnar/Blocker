using UnityEngine;

[CreateAssetMenu(menuName ="Effects/Height limit")]
public class RfxLimitHeight : ScienceEffect {


    const float PERCENT50 = 0.5f;

    [SerializeField] private TransformVarValue target;

    [SerializeField] private bool applyTransformDirection;
    [SerializeField] private bool applyInverseTransformDirection;
    [SerializeField] private Vec3VarRef change;

    [SerializeField] private FloatVarRef relativeHeight;
    [SerializeField] private FloatVarRef relativeHeightMultiplier;
    [SerializeField] private float minHeight = 0;
    [SerializeField] private float maxHeight = 0;
    [SerializeField] private Vec3VarRef result;

    protected override void Effect(ScienceArgs args)
    {
        Debug.LogError("make sure to use TransformDirection instead.");
        float targetHeight = relativeHeight.Value * (maxHeight - minHeight);
        float diff = targetHeight - target.Value.position.y;
        result.SetY(minHeight + diff);
    }
}
