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
    void AddStatUpgrade(UpgradeData upgradeMods);
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

    void OnDestroy()
    {
        // might throw an error when it's not used, but get's destroyed.
        if (upgradeMods != null)
            upgradeMods.RemoveObserver(this);
        else Debug.Log("Upgrade logs is already null, can't remove observer.");
    }

    public override void Notified(IModData value)
    {
        if (!IsUsed) return;
        // Object this stat mod is listening to, gets new value. Mirror it in this mod.
        if (value.ModType != statType)
            return;
        else Debug.Log("Attemting to add mod to incorrect stat."+value.ModType+statType);

        if (value as UpgradeData != null)
            AddStatUpgrade((UpgradeData)value);
        else if (value as Bonus != null)
            SwapWeaponBonus((Bonus)value);
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

    public void SwapWeaponBonus(Bonus bonus)
    {
        if (!IsUsed) return;
        weaponMods.ClearBonusMods();
        AddStat(bonus, weaponMods);
    }

    public void AddStatUpgrade(UpgradeData upgrade)
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
        OnAddMods(upgradeMods);
    }

    public void AddUpgrades(UpgradeMod upgrades)
    {
        if (!IsUsed) return;
        upgradeMods.AddMods(upgrades);
        upgrades.AddObserver(this);
        OnAddMods(upgradeMods);
    }

    protected virtual void OnAddMods(UpgradeMod userMods)
    {
    }

}
