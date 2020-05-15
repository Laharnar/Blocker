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
    public ButtonsList btns;

    public void OnClick(int optionId)
    {
        int cost = 0;
        int money = 0;
        if (money >= cost)
        {
            btns.Interact(new UICode()
            {
                userId = -1,
                data = new UpgradeData()
                {
                    upgradeId = optionId,
                    increase = data[optionId].increase
                }
            });
        }
    }
}