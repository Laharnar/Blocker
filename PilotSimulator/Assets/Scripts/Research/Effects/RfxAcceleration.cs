using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Operations/Vectors")]
public class RfxAcceleration:ScienceEffect {

    [Header("vector")]
    public BoolVarValue useX, useY, useZ;
    public Vec3VarRef ifNoneUsedFalse;
    public Vec3VarRef[] vectorOperations;

    [Header("quaternion")]
    public bool useRX;
    public FloatVar rotationStrengthY;
    public BoolVar directionRight;
    public OperatorTransitional op;

    [Header("logs")]
    public Vec3VarRef moveStart;
    public Vec3VarRef moveResult;

    protected override void Effect(ScienceArgs item)
    {
        // keeps old value, and adds through operation new values.
        Vector3 items = item.moveDir.Value;
        moveStart.Value = items;
        for (int i = 0; i < vectorOperations.Length; i++)
        {
            if (useX.Value)
            {
                items.x = TwoValueOperation.Operate(op.Value, items.x, vectorOperations[i].Value.x);
            }
            if (useY.Value)
            {
                items.y = TwoValueOperation.Operate(op.Value, items.y, vectorOperations[i].Value.y);
            }
            if (useZ.Value)
            {
                items.z = TwoValueOperation.Operate(op.Value, items.z, vectorOperations[i].Value.z);
            }
        }

        if (useX.Value || useY.Value || useZ.Value)
        {
            item.moveDir.Value = items;
            moveResult.Value = items;
        }
        else
        {
            moveResult.Value = ifNoneUsedFalse.Value;
            item.moveDir.Value = ifNoneUsedFalse.Value;
        }

        //item.moveX = moveStrengthZ.value; // remove when it works
        if (useRX)item.rotationDirY = rotationStrengthY.Value
            * (directionRight.value ? 1 : -1);
    }

}
