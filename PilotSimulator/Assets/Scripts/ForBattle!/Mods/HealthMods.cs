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

    protected override void OnAddMods(UpgradeMod userMods)
    {
        // load items from mods into hp.
        healthMods = userMods.GetModSum();
        healthSrc.health.Value = MaxHp;
        healthSrc.maxHealth.Value = MaxHp;
    }

    public override void Notified(IModData modValue)
    {
        base.Notified(modValue);
        // Heal amount that was added to max hp as mod.
        // Max hp doesn't have to be increased, because userMods.GetModSum()
        // automatically takes care of that.
        healthSrc.health.Value += (int)modValue.ModValue;
        healthSrc.maxHealth.Value += (int)modValue.ModValue;
        Debug.Log("inc hp+1 by mod");
    }
}
