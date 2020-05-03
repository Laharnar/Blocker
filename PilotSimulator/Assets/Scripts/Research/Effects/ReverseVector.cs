using UnityEngine;

[System.Serializable]
public class ReverseVector
{
    public BoolVar reverseX;
    public BoolVar reverseY;
    public BoolVar reverseZ;

    public Vector3 Reverse(Vector3 v)
    {
        if (reverseX) v.x = reverseX.value ? -1 : 1;
        if (reverseY) v.y = reverseY.value ? -1 : 1;
        if (reverseZ) v.z = reverseZ.value ? -1 : 1;
        return v;
    }
}
