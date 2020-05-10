using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class ScienceAffected : ModificationSource, ITestable
{
    public BoolVarValue used;
    public RealtimeScienceArgs ScienceTree;
    public List<ScienceEffect> Effects;
    public List<SimpleEffect> SimpleEffects;

    public UnityEvent onDone;

    public override void Modifications()
    {
        if (!used.Value) return;
        
        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].ActivateEffect(ScienceTree.args);
        }

        for (int i = 0; i < SimpleEffects.Count; i++)
        {
            SimpleEffects[i].Effect();
        }
        onDone?.Invoke();
    }

    public void TestInitialState()
    {
        RealtimeTester.Assert(Effects.Count == 0, this, "Data Transition From science effects to Simple Effects isn't complete.");
    }
}
