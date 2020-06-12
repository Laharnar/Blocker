using UnityEngine;

public class AttackStructure : MonoBehaviour, ITactic
{
    public bool used = false;
    [SerializeField] CombatController combatant;
    [SerializeField] TacticalUnit unit;

    CombatUser AttackBaseTarget {
        get {
            return attackedEnemyOrBoss = unit.EnemyBossToAttack;
        }
    }

    [Header("|Realtime|")]
    [SerializeField] CombatUser attackedEnemyOrBoss;

    private void Update()
    {
        if (used) Simulate();
    }

    public void Activate() { used = true; }

    public void Deactivate()
    {
        used = false;
        combatant.Stop();
        OnLoseFocusTactic.Do(combatant);
    }

    public void Simulate()
    {
        if (!used)
        {
            return;
        }

        if (attackedEnemyOrBoss && combatant.IsBlocked)
        {
            attackedEnemyOrBoss = combatant.SearchEnemy();
            combatant.NormalAttack(attackedEnemyOrBoss);
        }
        else
        {
            if (AttackBaseTarget)
                combatant.NormalAttack(AttackBaseTarget);
            else Debug.Log("Missing base to attack.");
        }
    }
}