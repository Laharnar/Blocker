using System;
using UnityEngine;

[CreateAssetMenu]
public class SimpleUpgrades : ScriptableObject
{
    public int attack = 0;
    public int health = 0;
    public float speed = 0;


    internal void Increase(UpgradeClick.UpgradeData data)
    {
        if (data.upgradeId == 0)
        {
            attack += (int)data.increase;
        }
        if (data.upgradeId == 1)
        {
            health += (int)data.increase;
        }
        if (data.upgradeId == 2)
        {
            speed += data.increase;
        }
    }
    public void FullReset(SimpleUpgrades resetValues)
    {
        attack = resetValues.attack;
        health = resetValues.health;
        speed = resetValues.speed;
    }
}