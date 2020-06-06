using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows mono behavours to have custom controlled update.
/// </summary>
/// <seealso cref="CombatScript"/> for example implementation
public interface ITickable 
{
    void Tick();
}
