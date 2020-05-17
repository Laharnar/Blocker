using System;
using UnityEngine;
using UnityEngine.Events;
public class HealthMods : MonoUserMods, IHealth
{
    public Health healthSrc;

    public int Hp {
        get {
            return healthSrc.Hp;
        }
    }

    public int MaxHp {
        get {
            return healthSrc.MaxHp + (int)userMods.GetModSum();
        }
    }

    public override void InitMods(IUserMods userMods)
    {
        base.InitMods(userMods);
        healthSrc.health.Value = MaxHp;
        healthSrc.maxHealth.Value = MaxHp;
    }

    public override void ModAdded(float modValue)
    {
        // Heal amount that was added to max hp as mod.
        // Max hp doesn't have to be increased, because userMods.GetModSum()
        // automatically takes care of that.
        healthSrc.health.Value += (int)modValue;
        healthSrc.maxHealth.Value += (int)modValue;
        Debug.Log("inc hp+1 by mod");
    }
}
