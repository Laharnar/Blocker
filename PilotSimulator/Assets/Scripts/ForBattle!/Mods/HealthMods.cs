using System;
using UnityEngine;
using UnityEngine.Events;
public class HealthMods : StatMods, IHealth, IValueGetter
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
            return healthSrc.MaxHp;
        }
    }

    protected override void OnAddedMod()
    {
        // load items from mods into hp.
        healthMods = GetSum();
        healthSrc.SetBonusMaxHpAndRecalcHp((int)healthMods);
        Debug.Log("set mods to "+ healthSrc.Hp);
    }

    public override void Notified(IModData modValue)
    {
        base.Notified(modValue);
        // Heal amount that was added to max hp as mod.
        // Max hp doesn't have to be increased, because userMods.GetModSum()
        // automatically takes care of that.
        healthSrc.SetBonusMaxHpAndRecalcHp((int)modValue.ModValue);
        Debug.Log("set mods to 2 "+ healthSrc.Hp);
    }

    public float GetValue(int id)
    {
        if (id == 0)
            return Hp;
        if (id == 1)
            return MaxHp;
        return 0;
    }
}
