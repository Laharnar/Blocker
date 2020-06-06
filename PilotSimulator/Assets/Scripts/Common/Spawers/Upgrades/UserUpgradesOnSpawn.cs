using UnityEngine;

public class UserUpgradesOnSpawn : MonoBehaviour, ISpawnUpgradeInitializer
{
    // prefab based stat loading.

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
        //Debug.Log("changes "+ upgrades.attack.ModCount);
        if (attacking) ((IStatAdder)attacking).AddUpgrades(upgrades.attack);
        if (health) ((IStatAdder)health).AddUpgrades((upgrades.health));
        if (movement) ((IStatAdder)movement).AddUpgrades(upgrades.speed);
    }
}
