using System.Collections.Generic;
using UnityEngine;

public class BuyingUpgrades:MonoBehaviour
{
    [SerializeField] List<SimpleUpgrades> upgradesOfUsers;

    [SerializeField] UpgradableAlliance upgradable;

    public void UpgradeUser(int user, UpgradeData data)
    {
        UpgradableUser userUps = upgradable.GetUser(user);
        data.SetUpgradeLevel(upgradesOfUsers[user].GetLevel(data.upgradeId));
        data.SetCosts(userUps.LoadCosts(data.upgradeId, data.upgradeLevel));
        data.SetMoney(userUps.LoadExp());
        int level = data.upgradeLevel;
        int cost = data.cost;
        int money = data.money;
        if (money >= cost)
        {
            Debug.Log("ClickUpgrade :: on " + user + " upId: " + data.upgradeId);
            upgradesOfUsers[user].Increase(data);
            userUps.SubtractExp(cost);
        }
        else
        {
            Debug.LogFormat("Not enough money to upgrade on {0} upgradeId: {1} cost: {2} exp: {3} level:{4}", user, data.upgradeId, cost, money, level);
        }
    }
}
