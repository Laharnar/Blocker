using UnityEngine;

[System.Serializable]
public class Vector3Setter
{
    [SerializeField] Vec3VarRef vec;


    public void SetToX(float x)
    {
        vec.Value = new Vector3(x, vec.Value.y, vec.Value.z);
    }
    public void SetToY(float y)
    {
        vec.Value = new Vector3(vec.Value.x, y, vec.Value.z);
    }
    public void SetToZ(float z)
    {
        vec.Value = new Vector3(vec.Value.x, vec.Value.y, z);
    }

    public Vector3 Value { get => vec.Value; }
}
