using UnityEngine;

public class UpgradeInit:MonoBehaviour, IInitializer
{
    public CombatController attacking;
    public Health survival;
    public PositionRotation movement;

    public void InitOnSpawn(string code, object args)
    {
        if (code == "upgrades")
        {
            SimpleUpgrades upgrades = (SimpleUpgrades)args;
            // replace increase flat values with modifiers sometime.
            // class 1: list of modifiers. hp.get damage -> call modifiers.
            attacking.IncreaseDamage(upgrades.attack);
            survival.IncreaseMaxHp(upgrades.health);
            movement.IncreaseMovementSpeed(upgrades.speed);
        }
    }

}