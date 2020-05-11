using System;
using UnityEngine;

public class CombatUser:MonoBehaviour
{
    public Alliance alliance;
    public Health hp;

    public void Damage(AttackAction basicAttackDamage)
    {
        hp.RecieveDamage(basicAttackDamage.damage);
    }
}
