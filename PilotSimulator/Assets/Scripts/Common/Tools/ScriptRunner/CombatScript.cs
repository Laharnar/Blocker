using System;
using System.Collections;
using UnityEngine;

public abstract class CombatScript:MonoBehaviour, ITickable
{
    protected abstract void CombatUpdate();

    protected virtual void CombatStart() { }

    protected virtual void CombatDestroy() { }

    // Don't override!
    protected void Start()
    {
        TickRunner.EnsureConnection(this);
        CombatStart();
    }

    // Don't override!
    protected void OnDestroy()
    {
        CombatDestroy();
        TickRunner.Disconnection(this);
    }

    public void Tick()
    {
        if (!PauseGlobal.Instance.IsPaused)
        {
            CombatUpdate();
        }
    }
}
