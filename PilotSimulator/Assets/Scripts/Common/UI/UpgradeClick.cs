using UnityEngine;

public class UpgradeClick:MonoBehaviour
{
    [System.Serializable]
    public class UpgradeData
    {
        [SerializeField]internal int upgradeId = 0;
        [SerializeField]internal float increase = 1;
    }
    public UpgradeData[] data;
    public PlaceHolderView placeholder;

    public void OnClick(int optionId)
    {
        int cost = 0;
        int money = 0;
        if (money >= cost)
        {


            placeholder.Interact(new UICode()
            {
                context = "ClickUpgrade",
                userId = UICode.ACTIVEUSER,
                data = new UpgradeData()
                {
                    upgradeId = optionId,
                    increase = data[optionId].increase
                }
            });
        }
    }
}