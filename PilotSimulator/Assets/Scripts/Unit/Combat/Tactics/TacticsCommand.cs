using System;
using System.Collections.Generic;
using UnityEngine;


public class TacticsCommand:MonoBehaviour
{
    public bool useTactic = true;
    [SerializeField] int devManualActiveTactic;
    [SerializeField] int activeTactic;
    [SerializeField] int lastTactic;
    [SerializeField] Transform defendingBase;
    [SerializeField] public CombatUser enemyBoss;
    public List<TacticGroup> units = new List<TacticGroup>();

    private void Update()
    {
        // for dev
        if (activeTactic != devManualActiveTactic)
            ChangeTacticAndActivate(devManualActiveTactic);

        for (int i = 0; i < units.Count; i++)
        {
            units[i].AllyBaseToDefend = defendingBase;
        }

        for (int i = 0; i < units.Count; i++)
        {
            units[i].EnemyBossToAttack = enemyBoss;
        }
    }


    public void DisconnectUnitOnDestroy(TacticGroup tacticGroup)
    {
        if (!units.Remove(tacticGroup))
            Debug.Log("Disconnected unit from commander when destroyed.");
    }

    public void ChangeTacticAndActivate(int i)
    {
        activeTactic = i;
        Activate(activeTactic);
    }

    internal void ConnectUnit(TacticGroup spawnedTactics)
    {
        if (spawnedTactics)
        {
            units.Add(spawnedTactics);
            spawnedTactics.officer = this;

            // ensure untis have correct tactic when they are added to command
            Activate(activeTactic);
        }
    }

    private void Activate(int id)
    {
        if (!useTactic) return;

        devManualActiveTactic = id;

        for (int i = 0; i < units.Count; i++)
        {
            units[i].Activate(id);
        }
    }
}
