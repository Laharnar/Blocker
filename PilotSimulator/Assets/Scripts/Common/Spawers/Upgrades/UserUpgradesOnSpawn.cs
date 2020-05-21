using UnityEngine;

public class UserUpgradesOnSpawn:MonoBehaviour, ISpawnInitializer
{
    public SimpleUpgrades defaultUpgradeSet;

    public MonoUserMods attacking;
    public MonoUserMods health;
    public MonoUserMods movement;

    private void Start()
    {
        if (defaultUpgradeSet)
        {
            if (attacking) attacking.InitMods(defaultUpgradeSet.attack);
            if (health) health.InitMods(defaultUpgradeSet.health);
            if (movement) movement.InitMods(defaultUpgradeSet.speed);
        }
    }

    public void InitOnSpawn(string code, object args)
    {
        if (code == "upgrades")
        {
            SimpleUpgrades upgrades = (SimpleUpgrades)args;
            // replace increase flat values with modifiers sometime.
            // class 1: list of modifiers. hp.get damage -> call modifiers.
            if (attacking)attacking.InitMods(upgrades.attack);
            if (health)   health.InitMods(upgrades.health);
            if (movement) movement.InitMods(upgrades.speed);
        }
    }
}
