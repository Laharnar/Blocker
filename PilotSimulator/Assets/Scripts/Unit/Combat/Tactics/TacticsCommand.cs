using System;
using System.Collections.Generic;
using UnityEngine;


public class TacticsCommand:MonoBehaviour
{
    // Tells which tactics or global targets should the commanded units go for.

    [Header("Realtime | Tactics")]
    public bool useTactic = true;
    [SerializeField] int devManualActiveTactic;
    [SerializeField] int activeTactic;

    [Header("Realtime | Commanding bases")]
    [SerializeField] Transform defendingBase;
    [SerializeField] public CombatUser enemyBoss;

    [Header("Realtime | Connected units")]
    public List<TacticalUnit> units = new List<TacticalUnit>();

    private void Update()
    {
        // for dev
        if (activeTactic != devManualActiveTactic)
            ChangeTacticAndActivate(devManualActiveTactic);

        InformAboutDefendedBase(defendingBase);
        InformAboutAttackableBase(enemyBoss);
    }

    private void InformAboutAttackableBase(CombatUser enemyBoss)
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].EnemyBossToAttack = enemyBoss;
        }
    }

    private void InformAboutDefendedBase(Transform defendingBase)
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].AllyBaseToDefend = defendingBase;
        }
    }

    public void DisconnectUnitOnDestroy(TacticalUnit tacticGroup)
    {
        if (!units.Remove(tacticGroup))
            Debug.Log("Disconnected unit from commander when destroyed.");
    }

    public void ChangeTacticAndActivate(int i)
    {
        activeTactic = i;
        Activate(activeTactic);
    }

    internal void ConnectUnit(TacticalUnit spawnedTactics)
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
