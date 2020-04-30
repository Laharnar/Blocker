using UnityEngine;

public class SetPosition:MonoBehaviour
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
        if(useInUpdate)
            SetToSelectedPosition();
    }

    public void Next()
    {
        if(rng.useRandom.Value)
           rng.NextRandom();
        else if(relativePositions.Length != 0) 
            active.Value = (active.Value+1)%relativePositions.Length;
    }

    public void SetToSelectedPosition()
    {
        if (rng.useRandom.Value)
        {
            target.Value.localPosition = lerping.ApplyIfUsed( target.Value.localPosition, rng.Value);
        }
        else
        {
            target.Value.localPosition = lerping.ApplyIfUsed(
                target.Value.localPosition,
                relativePositions[active.Value]);
        }
    }

}
