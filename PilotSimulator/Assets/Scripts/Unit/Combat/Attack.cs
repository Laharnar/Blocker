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
        if(optionalStats)
            logModsDamage = (int)optionalStats.BonusAttack;
        return damage + logModsDamage;
    }
}
