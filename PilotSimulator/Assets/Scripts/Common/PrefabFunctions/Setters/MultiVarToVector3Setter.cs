using UnityEngine;


[CreateAssetMenu(menuName = "Setters/NumToVec3ToNum")]
public class MultiVarToVector3Setter : ScriptableObject
{
    [SerializeField] MultiTypeValue numberX;
    [SerializeField] MultiTypeValue numberY;
    [SerializeField] MultiTypeValue numberZ;
    [SerializeField] bool numberXToX;
    [SerializeField] bool numberYToY;
    [SerializeField] bool numberZToZ;
    [SerializeField] bool xToNumberX;
    [SerializeField] bool yToNumberY;
    [SerializeField] bool zToNumberZ;
    [SerializeField] Vector3Setter vector;

    public Vector3 VecValue { get => vector.Value; }

    public void Activate()
    {
        if (numberXToX)
        {
            vector.SetToX(numberX.Value);
        }
        if (numberYToY)
        {
            vector.SetToY(numberY.Value);
        }
        if (numberZToZ)
        {
            vector.SetToZ(numberZ.Value);
        }

        if (xToNumberX)
        {
            numberX.Value = vector.Value.x;
        }
        if (yToNumberY)
        {
            numberY.Value = vector.Value.y;
        }
        if (zToNumberZ)
        {
            numberZ.Value = vector.Value.z;
        }
    }
}
