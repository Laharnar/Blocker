using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class StagedDelay
{
    public FloatVarRef delay;
    public bool[] stages;

    public IEnumerator Delay(int stageId)
    {
        if (stageId >= stages.Length || stages[stageId])
        {
            yield return new WaitForSeconds(delay.Value);
        }
    }
}
