using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class ScienceAffected : ModificationSource
{
    public BoolVarValue used;
    public RealtimeScienceArgs ScienceTree;
    public List<ScienceEffect> Effects;

    public UnityEvent onDone;

    public override void Modifications()
    {
        if (!used.Value) return;
        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].ActivateEffect(ScienceTree.args);
        }
        onDone?.Invoke();
    }

}
