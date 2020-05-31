using System;
using UnityEngine;

public class CombatUser : MonoBehaviour
{
    public Alliance alliance;
    public Health hp;
    public CombatController con8;

    public bool IsAttackable { get {
            return !hp.destroyed;
        }
    }

    public void Damage(int damage)
    {
        hp.RecieveDamage(damage);
    }
}
