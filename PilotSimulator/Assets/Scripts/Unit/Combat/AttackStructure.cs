using UnityEngine;

public class AttackStructure : MonoBehaviour, ITactic
{
    public bool used = false;
    [SerializeField] CombatController combatant;
    [SerializeField] TacticGroup unit;

    CombatUser AttackBaseTarget {
        get {
            return logBaseTarget = enemyToFollow = unit.EnemyBossToAttack;
        }
    }

    [Header("|Realtime|")]
    [SerializeField] CombatUser enemyToFollow;

    [Header("|Log|")]
    public CombatUser logBaseTarget;

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
        if (!enemyToFollow)
        {
            enemyToFollow = combatant.SearchEnemy();
        }

        if (enemyToFollow && combatant.IsBlocked)
            combatant.NormalAttack(enemyToFollow);
        else
        {
            AttackBossStructure();
        }
    }

    private void AttackBossStructure()
    {
        if (AttackBaseTarget)
            combatant.NormalAttack(AttackBaseTarget);
        else Debug.Log("Missing enemy.");
    }
}