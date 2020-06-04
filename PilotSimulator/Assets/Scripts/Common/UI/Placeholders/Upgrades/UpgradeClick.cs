using System.Collections.Generic;
using UnityEngine;
public class UpgradeClick:MonoBehaviour, ITestable
{

    [SerializeField] UpgradeData[] data;
    public List<PlaceHolderView> placeholder;

    public string GetUpgradeType(int i) {
        return data[i].upgradeType;
    }

    public void OnClick(int optionId)
    {
        UpgradeData dat = data[optionId].GetDataCopyForUpgradingUnit();
        for (int i = 0; i < placeholder.Count; i++)
        {
            placeholder[i].ResponseHandler(new ResponseToClick()
            {
                context = "ClickUpgrade",
                userId = ResponseToClick.ACTIVEUSER,
                data = dat
            });
        }
    }

    public void TestInitialState()
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i].TestInitialState(this);
        }
    }
}