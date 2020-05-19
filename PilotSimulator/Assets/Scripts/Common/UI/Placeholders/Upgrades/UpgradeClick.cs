using UnityEngine;
public class UpgradeClick:MonoBehaviour, ITestable
{

    [SerializeField] UpgradeData[] data;
    public PlaceHolderView placeholder;

    public string GetUpgradeType(int i) {
        return data[i].upgradeType;
    }

    public void OnClick(int optionId)
    {
        int cost = 0;
        int money = 0;
        if (money >= cost)
        {
            placeholder.ResponseHandler(new ResponseToClick()
            {
                context = "ClickUpgrade",
                userId = ResponseToClick.ACTIVEUSER,
                data= data[optionId].Copy()
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