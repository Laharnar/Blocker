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
        CombatStart();
    }

    private void OnEnable()
    {
        TickRunner.EnsureConnection(this);
    }

    private void OnDisable()
    {
        TickRunner.Disconnection(this);
    }

    // Don't override!
    protected void OnDestroy()
    {
        CombatDestroy();
    }

    public void Tick()
    {
        if (!PauseGlobal.Instance.IsPaused)
        {
            CombatUpdate();
        }
    }
}
