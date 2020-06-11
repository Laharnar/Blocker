using UnityEngine;

[System.Serializable]
public class AttackAction
{
    public int damage;
    [SerializeField] int logModsDamage;
    [SerializeField] ExpandedStats optionalStats;

    public AttackAction(int damage)
    {
        this.damage = damage;
    }

    public int GetDamage()
    {
        int bonus = 0;
        if (optionalStats != null)
        {
            bonus = (int)optionalStats.BonusAttack;
            logModsDamage = bonus;
        }
        return damage + bonus;
    }
}
