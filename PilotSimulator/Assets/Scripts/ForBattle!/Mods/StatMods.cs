using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatGetter
{
    float GetSum();
}
public interface IStatAdder
{
    void AddUpgradeStats(UpgradeData upgradeMods);
    void AddUpgradeStats(UpgradeMod upgradeMods);
}

public class StatMods: ValueChangeNotifiable, IStatGetter, IStatAdder
{
    [SerializeField] float logSum = 0;
    [SerializeField] protected UpgradeMod upgradeMods;
    [SerializeField] protected UpgradeMod weaponMods;
    [SerializeField] bool init = false;

    // public string StatType { get; internal set; }

    void OnDestroy()
    {
        if (upgradeMods != null)
            upgradeMods.RemoveObserver(this);
        else Debug.Log("Upgrade logs is already null, can't remove observer.");
    }

    public override void Notified(IModData value)
    {
        // Object this stat mod is listening to, gets new value. Mirror it in this mod.
        Debug.Log("(default) Notified about new value " + value);
        if (value as UpgradeData != null)
            AddUpgradeStats((UpgradeData)value);
        else if (value as Bonus != null)
            AddStatBonus((Bonus)value);
        else Debug.LogError("Unhnadled transfered type."+ value, this);
    }

    public float GetSum()
    {
        logSum = 0;
        if (upgradeMods != null)
            logSum += upgradeMods.GetModSum();
        if (weaponMods != null)
            logSum += weaponMods.GetModSum();
        return logSum;
    }

    public void AddStatBonus(Bonus bonus)
    {
        weaponMods.AddMod(bonus);
    }

    public void AddUpgradeStats(UpgradeData upgrade)
    {
        upgradeMods.AddMod(upgrade);
        OnAddMods(upgradeMods);
    }

    public void AddUpgradeStats(UpgradeMod upgrades)
    {
        upgradeMods.AddMods(upgrades);
        upgrades.AddObserver(this);
        OnAddMods(upgradeMods);
    }

    protected virtual void OnAddMods(UpgradeMod userMods)
    {
    }

}
