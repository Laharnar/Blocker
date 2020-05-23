using UnityEngine;

public class UserUpgradesOnSpawn:MonoBehaviour, ISpawnUpgradeInitializer
{
    public SimpleUpgrades defaultUpgradeSet;

    public StatMods attacking;
    public StatMods health;
    public StatMods movement;

    private void Start()
    {
        if (defaultUpgradeSet)
        {
            InitUpgradesOnSpawn(defaultUpgradeSet);
            //if (attacking) attacking.InitMods(defaultUpgradeSet.attack);
            //if (health) health.InitMods(defaultUpgradeSet.health);
            //if (movement) movement.InitMods(defaultUpgradeSet.speed);
        }
    }
    public void InitUpgradesOnSpawn(SimpleUpgrades upgrades)
    {
        if (attacking) attacking.SetMods(upgrades.attack);
        if (health) health.SetMods(upgrades.health);
        if (movement) movement.SetMods(upgrades.speed);
    }
}
