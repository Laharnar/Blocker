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
    void AddUnitStatUpgrade(UpgradeData upgradeMods);
    void AddUpgrades(UpgradeMod upgradeMods);
}

public class StatMods : ValueChangeNotifiable, IStatGetter, IStatAdder
{
    [SerializeField] float logSum = 0;
    [SerializeField] protected UpgradeMod upgradeMods;
    [SerializeField] protected UpgradeMod weaponMods;
    [SerializeField] bool init = false;
    [SerializeField] string statType;
    public string StatType { get => statType; }
    bool IsUsed => this!= null && gameObject.activeInHierarchy;
    public static UpgradeMod badCode_lastMod;

    void OnDestroy()
    {
        // might throw an error when it's not used, but get's destroyed.
        if (upgradeMods != null)
            upgradeMods.RemoveObserver(this);
        else Debug.Log("Upgrade logs is already null, can't remove observer.");
    }

    // Notified is used for events, that want to trigger upgrading.
    // For inheritance don't override Notified. Use OnAddMods instead.
    public override void Notified(IModData value)
    {
        if (!IsUsed) return;
        // Object this stat mod is listening to, gets new value. Mirror it in this mod.
        if (value.ModType != statType)
            return;
        else Debug.Log("Attemting to add mod to incorrect stat."+value.ModType+statType);

        if (value as UpgradeData != null)
            AddUnitStatUpgrade((UpgradeData)value);
        else if (value as Bonus != null)
            SwapWeaponBonuses((Bonus)value);
        else Debug.LogError("Unhnadled transfered type."+ value, this);
    }

    public float GetSum()
    {
        logSum = 0;
        if (IsUsed)
        {
            if (upgradeMods != null)
                logSum += upgradeMods.GetModSum();
            if (weaponMods != null)
                logSum += weaponMods.GetModSum();
        }
        return logSum;
    }

    public void SwapWeaponBonuses(Bonus bonus)
    {
        if (!IsUsed) return;
        weaponMods.ClearBonusMods();
        AddStat(bonus, weaponMods);
    }

    public void AddUnitStatUpgrade(UpgradeData upgrade)
    {
        if (!IsUsed) return;
        AddStat(upgrade, upgradeMods);
    }

    public void AddStat(IModData modItem, UpgradeMod mod)
    {
        if (!IsUsed) return;
        if (modItem.ModType != statType)
            return;
        mod.AddMod(modItem);
        badCode_lastMod = mod;
        OnAddedMod();
    }

    public void AddUpgrades(UpgradeMod upgrades)
    {
        if (!IsUsed) return;
        upgradeMods.AddMods(upgrades);
        upgrades.AddObserver(this);
        badCode_lastMod = upgrades;
        OnAddedMod();
    }

    protected virtual void OnAddedMod()
    {

    }
}
