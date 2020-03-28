using System;
using System.Collections.Generic;
using UnityEngine;

// to create new effect make a class deriving from this class, and add [CreateAssetMenu]
public abstract class ScienceEffect:ScriptableObject {

    protected abstract void Activate(ScienceArgs args);

    // New values should be saved in args.
    internal static void RequestScienceEffect(List<ScienceEffect> effects, ScienceArgs args)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Activate(args);
        }
    }
}
