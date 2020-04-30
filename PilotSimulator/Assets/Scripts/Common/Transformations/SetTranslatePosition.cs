using UnityEngine;

public class SetTranslatePosition : MonoBehaviour
{
    public TransformVarValue target;
    public bool useInUpdate = true;
    public LerpStrength lerping;

    public RandomInArea rng;
    [Header("Controlled")]
    public IntVarValue active;
    public Vector3[] relativePositions;

    private void Update()
    {
        if (useInUpdate)
            SetToSelectedPosition();
    }

    public void Next()
    {
        if(relativePositions.Length > 0)
            active.Value = (active.Value + 1) % relativePositions.Length;
        rng.NextRandom();
    }

    public void SetToSelectedPosition()
    {
        if (rng.useRandom.Value)
        {
            target.Value.Translate(lerping.ApplyIfUsed(target.Value.localPosition, rng.Value)
                * Time.deltaTime
                - target.Value.position);
        }
        else
        {
            target.Value.Translate(lerping.ApplyIfUsed(
                target.Value.localPosition,
                relativePositions[active.Value]) * Time.deltaTime
                - target.Value.position);
        }
    }

}