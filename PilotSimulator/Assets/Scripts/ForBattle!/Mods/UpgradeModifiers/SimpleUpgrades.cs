using System;
using UnityEngine;

[CreateAssetMenu]
public class SimpleUpgrades : UpgradePrefab
{
    public UpgradeMod attack;
    public UpgradeMod health; 
    public UpgradeMod speed;

    [SerializeField] int[] levels = new int[3];

    public int UpgradeCount { get => 3; }

    public override void Increase(UpgradeData data)
    {
        if (AddModifierByName(data, "Attack", attack)) return;
        else if (AddModifierByName(data, "Health", health)) return;
        else if (AddModifierByName(data, "Speed", speed)) return;
        else
        {
            Debug.LogError("Unhandled upgrade type " + data.upgradeType, this);
        }
    }

    internal int GetLevel(int upgradeId)
    {
        return levels[upgradeId];
    }

    private bool AddModifierByName(UpgradeData data, string title, UpgradeMod mods)
    {
        if (data.upgradeType == title)
        {
            levels[data.upgradeId]++;
            mods.AddMod(data);
            return true;
        }
        return false;
    }

    public void FullReset(SimpleUpgrades resetValues)
    {
        attack = resetValues.attack.GetDataCopyForResettingModSets();
        health = resetValues.health.GetDataCopyForResettingModSets();
        speed = resetValues.speed.GetDataCopyForResettingModSets();
        levels = new int[3];
    }
}