using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RfxAcceleration:ScienceEffect {

    public FloatVar moveStrengthZ;
    public FloatVar rotationStrengthY;
    public BoolVar rotationRight;
    
    protected override void Activate(ScienceArgs item)
    {
        // accelerate depending on research
        item.moveX = moveStrengthZ.value;
        item.rotationDirY = rotationStrengthY.value
            * (rotationRight.value ? 1 : -1);
    }

}
