using System;
using System.Collections.Generic;
using UnityEngine;

// to create new effect make a class deriving from this class, and add [CreateAssetMenu]
public abstract class ScienceEffect:ScriptableObject {

    protected abstract void Effect(ScienceArgs args);

    public void ActivateEffect(ScienceArgs args)
    {
        Effect(args);
    }
}
