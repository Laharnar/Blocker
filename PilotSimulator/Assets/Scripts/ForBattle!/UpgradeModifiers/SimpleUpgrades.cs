using System;
using UnityEngine;

[CreateAssetMenu]
public class SimpleUpgrades : UpgradePrefab
{
    public UpgradeMods attack;
    public UpgradeMods health; 
    public UpgradeMods speed;

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

    private bool AddModifierByName(UpgradeData data, string title, UpgradeMods mods)
    {
        if (data.upgradeType == title)
        {
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
    }
}