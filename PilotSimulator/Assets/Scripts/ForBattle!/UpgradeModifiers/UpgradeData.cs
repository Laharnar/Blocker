using System;
using UnityEngine;


[System.Serializable]
public class UpgradeData
{
    [SerializeField] internal int upgradeId = 0;
    [SerializeField] internal string upgradeType;
    [SerializeField] internal float increase = 1;
    [SerializeField] internal int cost;
    [SerializeField] internal int money;
    [SerializeField] internal int userId;

    [SerializeField] internal int upgradeLevel;

    internal void SetMoney(int v)
    {
        money = v;
    }

    internal void SetCosts(int v)
    {
        cost = v;
    }

    internal void SetUpgradeLevel(int v)
    {
        upgradeLevel = v;
    }

    public void TestInitialState(MonoBehaviour mono)
    {
        RealtimeTester.Assert(upgradeType != "", mono, "Upgrade type is empty. Assign it.");
    }

    internal UpgradeData Copy()
    {
        return new UpgradeData()
        {
            upgradeType = upgradeType,
            upgradeId = upgradeId,
            increase = increase,
            cost = cost,
            money = money,
            userId = userId
        };
    }
}
