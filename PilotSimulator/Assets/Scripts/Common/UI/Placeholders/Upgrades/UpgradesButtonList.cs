
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesButtonList : PlaceHolderView
{
    [SerializeField] List<SimpleUpgrades> upgradesOfUsers;
    [SerializeField] TMPro.TMP_Text title;
    [SerializeField] TMPro.TMP_Text[] buttons;
    int activeUser; // set active use when changing ui, then 


    public override void ResponseHandler(ResponseToClick response)
    {
        if (response.userId >= upgradesOfUsers.Count)
        {
            Debug.Log("Changed tree.");
            return;
        }
        if (response.context == "SelectUser")
        {
            SetUser(response.userId);
        }

        if (response.context == "ClickUpgrade")
        {
            UpgradeUser(activeUser, response.data);
        }

        buttons[0].text = "" + upgradesOfUsers[activeUser].attack.GetModSum();
        buttons[1].text = "" + upgradesOfUsers[activeUser].health.GetModSum();
        buttons[2].text = "" + upgradesOfUsers[activeUser].speed.GetModSum();
    }

    private void UpgradeUser(int user, UpgradeData data)
    {
        Debug.Log("ClickUpgrade :: on " + user + " upId: " + data.upgradeId);
        upgradesOfUsers[user].Increase(data);
    }


    // Can be used in button events.
    public void SetUser(int userId)
    {
        Debug.Log("Set user "+userId);
        activeUser = userId;
        title.text = "User" + (activeUser + 1);
    }

}
