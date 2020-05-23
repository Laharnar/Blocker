using System;
using UnityEngine;
using UnityEngine.Events;
public class HealthMods : StatMods, IHealth
{
    public Health healthSrc;
    [SerializeField] float healthMods;

    public int Hp {
        get {
            return healthSrc.Hp;
        }
    }

    public int MaxHp {
        get {
            return healthSrc.MaxHp + (int)upgradeMods.GetModSum();
        }
    }

    public override void SetMods(UpgradeMods userMods)
    {
        base.SetMods(userMods);
        healthMods = userMods.GetModSum();
        healthSrc.health.Value = MaxHp;
        healthSrc.maxHealth.Value = MaxHp;
    }

    public override void Notified(float modValue)
    {
        base.Notified(modValue);
        // Heal amount that was added to max hp as mod.
        // Max hp doesn't have to be increased, because userMods.GetModSum()
        // automatically takes care of that.
        healthSrc.health.Value += (int)modValue;
        healthSrc.maxHealth.Value += (int)modValue;
        Debug.Log("inc hp+1 by mod");
    }
}
