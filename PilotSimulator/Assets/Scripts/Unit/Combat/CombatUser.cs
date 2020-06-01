using System;
using UnityEngine;

public class CombatUser : MonoBehaviour, ITestable
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

    public void TestInitialState()
    {
        if(!Application.isPlaying)
            RealtimeTester.Assert(con8!= null, this, "con8 isn't assigned.");
    }
}
