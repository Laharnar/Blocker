using System;
using UnityEngine;

[CreateAssetMenu]
public class SimpleUpgrades : UpgradePrefab
{
    public UpgradeMods attack;
    public UpgradeMods health; 
    public UpgradeMods speed;

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

    private bool AddModifierByName(UpgradeData data, string title, UpgradeMods mods)
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
        attack = resetValues.attack.HardCopy();
        health = resetValues.health.HardCopy();
        speed = resetValues.speed.HardCopy();
        levels = new int[3];
    }
}