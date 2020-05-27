using System.Collections.Generic;
using UnityEngine;
public class CostUI:MonoBehaviour, IShowForUser
{
    [SerializeField] TMPro.TMP_Text[] costsTexts;
    [SerializeField] List<UpgradableUser> costs;
    [SerializeField] UpgradesButtonList ups;

    private void Start()
    {
        ShowCostForUser(0);
    }

    public void ShowCostForUser(int user) { 
        for (int i = 0; i < ups.upgradesOfUsers[user].UpgradeCount; i++)
        {
            int levelForUpgrade = ups.upgradesOfUsers[user].GetLevel(i);
            int cost = costs[user].LoadCosts(i, levelForUpgrade);
            ShowCostForUpgrade(i, cost);
        }
    }

    void ShowCostForUpgrade(int upgradeId, int cost)
    {
        costsTexts[upgradeId].text = "" + cost;
    }

    public void ShowUser(int i)
    {
        ShowCostForUser(i);
    }
}
