using System;
using UnityEngine;

public class CombatUser:MonoBehaviour
{
    public Alliance alliance;
    public Health hp;

    public void Damage(int damage)
    {
        hp.RecieveDamage(damage);
    }
}
