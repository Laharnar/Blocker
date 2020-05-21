using UnityEngine;

[System.Serializable]
public class AttackAction
{
    public int damage;
    [SerializeField] int logModsDamage;
    public AttackAction(int damage)
    {
        this.damage = damage;
    }

    public int GetDamage(IUserMods mods)
    {
        logModsDamage = (int)mods.GetModSum();
        return damage + logModsDamage;
    }
}
