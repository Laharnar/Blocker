using System;
using System.Collections.Generic;
using UnityEngine;

public class TacticGroup:MonoBehaviour
{

    [Header("Realtime | Tactics")]
    public bool useTactic = true;

    public TacticalBases commands;

    [Header("Realtime | Connected units")]
    public List<TacticallyConnected> units = new List<TacticallyConnected>();

    public List<TacticallyConnected> Units => units;
    [SerializeField] int activeTactic;
    public List<MonoConnection> onUnitDeathEvents;

    public void DisconnectUnitOnDestroy(TacticallyConnected tacticGroup)
    {
        if (!units.Remove(tacticGroup))
        {
            Debug.Log("Disconnection unit from commander when destroyed FAILED. Unit isn't in units.");
        }
        InformOnDeath();
    }

    internal void ActivateDefaultTactic()
    {
        ChangeTacticAndActivate(activeTactic);
    }

    internal void ConnectUnit(TacticallyConnected spawnedTactics)
    {
        if (spawnedTactics)
        {
            units.Add(spawnedTactics);
            spawnedTactics.officer = this;
        }
    }

    public void ChangeTacticAndActivate(int i)
    {
        activeTactic = i;
        Activate(activeTactic);
    }

    public void Activate(int id)
    {
        if (!useTactic) return;

        for (int i = 0; i < units.Count; i++)
        {
            units[i].Activate(id);
        }
    }

    public void InformOnDeath()
    {
        for (int i = 0; i < onUnitDeathEvents.Count; i++)
        {
            onUnitDeathEvents[i].onDeathEvents.OnDeath();
        }
    }
}
