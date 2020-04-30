using UnityEngine;

public class SetRotation : MonoBehaviour
{
    public TransformVarValue target;

    public Vector3[] rotations;
    public IntVarValue active;

    public bool useLocalAngles = true;
    public LerpStrength lerping;

    public bool setRotInUpdate = true;


    private void Update()
    {
        if (setRotInUpdate)
        {
            SetToRotation();
        }
    }

    public void Next()
    {
        active.Value = (active.Value + 1) % rotations.Length;
    }

    public void SetToRotation()
    {
        Vector3 newRot = rotations[active.Value];
        Vector3 lerpRef;
        if (useLocalAngles)
        {
            lerpRef = target.Value.localEulerAngles;
            target.Value.localEulerAngles = lerping.ApplyIfUsed(lerpRef, newRot);
        }
        else
        {
            lerpRef = target.Value.eulerAngles;
            target.Value.eulerAngles = lerping.ApplyIfUsed(lerpRef, newRot);
        }
    }
}
