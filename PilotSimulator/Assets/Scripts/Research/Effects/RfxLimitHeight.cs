using UnityEngine;

[CreateAssetMenu]
public class RfxLimitHeight : ScienceEffect {

    [SerializeField] BoolVarValue snapToRelativeHeight;
    public LinkedTransformSpace space;
    public bool applyTransformDirection;
    public bool applyInverseTransformDirection;
    [SerializeField] FloatVarRef relativeHeight;
    [SerializeField] FloatVarRef relativeHeightMultiplier;
    const float PERCENT50 = 0.5f;
    public float minHeight = 0;
    public float maxHeight = 0;

    protected override void Activate(ScienceArgs args)
    {
        space.UpdateUsedSpace();
        if (snapToRelativeHeight.Value)
        {
            

            if (applyTransformDirection)
                args.moveDir = args.source.TransformDirection(args.moveDir);
            if (applyInverseTransformDirection)
                args.moveDir = args.source.InverseTransformDirection(args.moveDir);

            float targetHeight = relativeHeight.Value * (maxHeight - minHeight);
            float diff = targetHeight - args.source.position.y;
            args.moveDir.y = minHeight + diff;
        }
    }
}
