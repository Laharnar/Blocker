using UnityEngine;

[CreateAssetMenu]
public class RfxLimitHeight : ScienceEffect {

    [SerializeField] BoolVarValue snapToRelativeHeight;
    public LinkedTransformSpace space;
    public TransformVarValue target;
    public bool applyTransformDirection;
    public bool applyInverseTransformDirection;
    [SerializeField] FloatVarRef relativeHeight;
    [SerializeField] FloatVarRef relativeHeightMultiplier;
    const float PERCENT50 = 0.5f;
    public float minHeight = 0;
    public float maxHeight = 0;
    [SerializeField] Vector3 result;

    protected override void Effect(ScienceArgs args)
    {
        space.UpdateUsedSpace();
        if (snapToRelativeHeight.Value)
        {
            

            if (applyTransformDirection)
                args.moveDir.Value = target.Value.TransformDirection(args.moveDir.Value);
            if (applyInverseTransformDirection)
                args.moveDir.Value = target.Value.InverseTransformDirection(args.moveDir.Value);

            float targetHeight = relativeHeight.Value * (maxHeight - minHeight);
            float diff = targetHeight - target.Value.position.y;
            args.moveDir.SetY(minHeight + diff);
            result = args.moveDir.Value;
        }
    }
}
