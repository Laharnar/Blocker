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
        AddModifier(data);
    }

    private void AddModifier(UpgradeData data)
    {
        if (data.upgradeType == "Attack")
        {
            attack.Add(data);
        }
        else if (data.upgradeType == "Health")
        {
            health.Add(data);
        }
        else if (data.upgradeType == "Speed")
        {
            speed.Add(data);
        }
        else
        {
            Debug.LogError("Unhandled upgrade type " + data.upgradeType, this);
        }
    }

    public void FullReset(SimpleUpgrades resetValues)
    {
        attack = resetValues.attack;
        health = resetValues.health;
        speed = resetValues.speed;
    }
}