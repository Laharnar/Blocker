using UnityEngine;
public interface IModData
{
    string ModType { get; }
    float ModValue { get; }
}
[System.Serializable]
public class UpgradeData: IModData
{
    [SerializeField] internal int upgradeId = 0;
    [SerializeField] internal string upgradeType;
    [SerializeField] internal float increase = 1;

    public string ModType { get => upgradeType; }
    public float ModValue { get => increase; }

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
        };
    }

    public UpgradeData GetDataCopyForUpgradingUnit()
    {
        return Copy();
    }
}
