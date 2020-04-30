using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RfxAcceleration:ScienceEffect {

    public BoolVarValue useX, useY, useZ;
    public FloatVar moveStrengthX;
    public FloatVar moveStrengthY;
    public FloatVar moveStrengthZ;
    public bool useRX;
    public FloatVar rotationStrengthY;
    public BoolVar directionRight;

protected override void Activate(ScienceArgs item)
    {
        if (useX.Value) item.moveDir.x = moveStrengthX.Value;
        if (useY.Value) item.moveDir.y = moveStrengthY.Value;
        if (useZ.Value) item.moveDir.z = moveStrengthZ.Value;
        //item.moveX = moveStrengthZ.value; // remove when it works
        if(useRX)item.rotationDirY = rotationStrengthY.Value
            * (directionRight.value ? 1 : -1);
    }

}
