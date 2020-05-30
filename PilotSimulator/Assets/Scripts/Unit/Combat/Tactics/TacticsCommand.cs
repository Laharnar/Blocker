using System;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCommand:MonoBehaviour, ISetupUnity
{
    public bool useTactic = true;
    [SerializeField] int devManualActiveTactic;
    [SerializeField] int activeTactic;
    [SerializeField] int lastTactic;

    public List<TacticGroup> units = new List<TacticGroup>();

    private void Update()
    {
        // for dev
        if (activeTactic != devManualActiveTactic)
            SetTacticByUI(devManualActiveTactic);
    }

    public void Activate()
    {
        if (!useTactic) return;

        for (int i = 0; i < units.Count; i++)
        {
            units[i].Activate(activeTactic);
        }
    }

    public void DisconnectUnitOnDestroy(TacticGroup tacticGroup)
    {
        if (!units.Remove(tacticGroup))
            Debug.Log("Disconnected unit from commander when destroyed.");
    }

    public void SetTacticByUI(int i)
    {
        activeTactic = i;
    }

    public void SetIsUsed(bool use)
    {
        useTactic = use;
    }

    public void Log()
    {
        Debug.Log("Logged tactic");
    }

    public bool UnitySetup()
    {
        if (units == null)
        {
            units = new List<TacticGroup>();
        }
        if (units.Count == 0)
        {
            TacticGroup tg = GetComponentInChildren<TacticGroup>();
            ConnectUnit(tg);
        }
        if (units.Count > 0)
            return true;
        return false;
    }

    internal void ConnectUnit(TacticGroup spawnedTactics)
    {
        Debug.Log("Adding unit to tactics command.");
        if (spawnedTactics)
        {
            units.Add(spawnedTactics);
            spawnedTactics.officer = this; 
        }
    }
}
