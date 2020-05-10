using System;
using System.Collections.Generic;
using UnityEngine;

// to create new effect make a class deriving from this class, and add [CreateAssetMenu]
public abstract class ScienceEffect: SimpleEffect
{
    protected abstract void Effect(ScienceArgs args);

    public void ActivateEffect(ScienceArgs args)
    {
        Effect(args);
    }

    protected override void DoEffect()
    {
        Debug.LogError("Calling simple effect on science effect is not allowed.", this);
    }
}

public abstract class SimpleEffect:ScriptableObject
{
    [SerializeField] BoolVarValue use;

    protected abstract void DoEffect();

    public void Effect()
    {
        if (use.Value)
        {
            DoEffect();
        }
    }
}