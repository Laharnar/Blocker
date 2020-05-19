using JetBrains.Annotations;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesButtonList : PlaceHolderView
{
    [SerializeField] List<SimpleUpgrades> upgradesOfUsers;
    [SerializeField] TMPro.TMP_Text title;
    [SerializeField] TMPro.TMP_Text[] buttons;
    public int activeUser; // set active use when changing ui, then 

    public override void ResponseHandler(ResponseToClick response)
    {
        if (response.userId >= upgradesOfUsers.Count)
        {
            Debug.Log("Changed tree.");
            return;
        }
        Debug.LogError("TestableDestroyableMono if upgrades work");

        if (response.context == "SelectUser")
        {
            Debug.Log("SelectUser :: "+activeUser);
            activeUser = response.userId;
            title.text = "User" + (activeUser + 1);
        }

        if (response.context == "ClickUpgrade")
        {
            Debug.Log("ClickUpgrade :: on "+activeUser+ " upId: "+response.data.upgradeId);
            upgradesOfUsers[activeUser].Increase(response.data);
        }

        buttons[0].text = ""+upgradesOfUsers[activeUser].attack.GetModSum();
        buttons[1].text = ""+upgradesOfUsers[activeUser].health.GetModSum();
        buttons[2].text = ""+upgradesOfUsers[activeUser].speed.GetModSum();
    }


    public void SetUser(int userId)
    {
        activeUser = userId;
    }
}
