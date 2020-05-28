using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableUser : MonoBehaviour
{
    [SerializeField] int alliance;
    // One per alliance.
    [SerializeField] List<Costs> upgrades = new List<Costs>();
    [SerializeField] ExpGroup collectedPoints;

    internal int LoadCosts(int upgradeId, int upgradeLevel)
    {
        return upgrades[upgradeId].GetCosts(upgradeLevel);
    }

    internal int LoadExp()
    {
        return collectedPoints.GetExp();
    }

    internal void SubtractExp(int cost)
    {
        if (collectedPoints == null) Debug.LogError("collectedPoints is null", this);
        else
        {
            collectedPoints.Decrease(new ExpGainArgs()
            {
                groupId = alliance,
                intValue = cost
            });
        }
    }

    internal static void Connect(UpgradableUser upgradableUser, ExpGroup expGain)
    {
        if (upgradableUser == null)
            Debug.LogError("User is null.");
        upgradableUser.collectedPoints = expGain;
    }
}
