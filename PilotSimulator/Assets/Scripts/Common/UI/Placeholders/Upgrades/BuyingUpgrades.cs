﻿using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BuyingUpgrades:MonoBehaviour
{
    [SerializeField] List<SimpleUpgrades> upgradesOfUsers;

    [SerializeField] UpgradableAlliance upgradable;

    public void UpgradeUser(int user, UpgradeData data)
    {
        UpgradableUser userUps = upgradable.GetUser(user);
        int level = upgradesOfUsers[user].GetLevel(data.upgradeId);
        int cost = userUps.LoadCosts(data.upgradeId, level);
        int money = userUps.LoadExp();
        if (money >= cost)
        {
            // Debug.Log("ClickUpgrade :: on " + user + " upId: " + data.upgradeId);
            upgradesOfUsers[user].Increase(data);
            userUps.SubtractExp(cost);
        }
        else
        {
            Debug.LogFormat("Not enough money to upgrade on {0} upgradeId: {1} cost: {2} exp: {3} level:{4}", user, data.upgradeId, cost, money, level);
        }
    }

    public int LastestCost(int user, int upgradeId)
    {
        UpgradableUser userUps = upgradable.GetUser(user);
        int lvl = upgradesOfUsers[user].GetLevel(user);
        int cost =userUps.LoadCosts(upgradeId, lvl);
        return cost;
    }
}
