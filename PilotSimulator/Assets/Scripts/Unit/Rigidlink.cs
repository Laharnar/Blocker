using System;
using UnityEngine;

[System.Serializable]
public class Rigidlink: IManualTest
{
    [SerializeField] Rigidbody rig;
    [SerializeField] Rigidbody2D rig2;

    Transform transform {
        get {
            if(rig != null)
            {
                return rig.transform;
            }
            return rig2.transform;
        }
    }

    public void MoveTo(Vector3 position)
    {
        if (rig) rig.MovePosition(position);
        if (rig2) rig2.MovePosition(position);
    }
    public void RotateTo(Quaternion quaternion)
    {
        if (rig) rig.MoveRotation(quaternion);
        if (rig2) rig2.MoveRotation(quaternion);
    }

    public void Set(Rigidbody rig)
    {
        this.rig = rig;
    }
    public void Set(Rigidbody2D rig2)
    {
        this.rig2 = rig2;
    }

    public void RunTests()
    {
        RealtimeTester.Assert(!IsNull(), this, "Rigidbody 2D or 3D isn't assigned.");
    }

    public bool IsNull()
    {
        return rig == null && rig2 == null;
    }

    public void MoveBy(Vector3 direction)
    {
        MoveTo(transform.position + direction);
    }

    public void RotateBy(Vector3 rotationAngles)
    {
         RotateTo(Quaternion.Euler(transform.eulerAngles + rotationAngles));
    }

}
