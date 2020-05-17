using UnityEngine;

public class UpgradeInit:MonoBehaviour, IInitializer
{
    public MonoUserMods attacking;
    public MonoUserMods health;
    public MonoUserMods movement;

    public void InitOnSpawn(string code, object args)
    {
        if (code == "upgrades")
        {
            SimpleUpgrades upgrades = (SimpleUpgrades)args;
            // replace increase flat values with modifiers sometime.
            // class 1: list of modifiers. hp.get damage -> call modifiers.
            attacking.InitMods(upgrades.attack);
            health.InitMods(upgrades.health);
            movement.InitMods(upgrades.speed);
        }
    }

}