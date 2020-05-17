using System;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    [SerializeField] internal int upgradeId = 0;
    [SerializeField] internal string upgradeType;
    [SerializeField] internal float increase = 1;

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
            increase = increase
        };
    }
}
