﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class TacticGroup:MonoBehaviour, ISetupUnity
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
            if (onUnitDeathEvents[i] != null)
            {
                if(onUnitDeathEvents[i].onDeathEvents != null)
                    onUnitDeathEvents[i].onDeathEvents.OnDeath();
                else Debug.LogError("Death event is null", this);
            }
            else Debug.LogError("Inform on death is null", this);
        }
    }

    public void SkillDamageGroup(int value)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if(units[i])
                units[i].scripts.hp.RecieveDamage(value);
        }
    }

    public bool UnitySetup()
    {
        for (int i = 0; i < onUnitDeathEvents.Count; i++)
        {
            RealtimeTester.Assert(onUnitDeathEvents[i] != null, this, "missing connected event. ", TestPriority.AllowInTestEnv);
            if(onUnitDeathEvents[i] == null)
            {

                return false;
            }
        }
        return true;
    }
}
