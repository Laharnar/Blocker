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

    public override void Interact(UICode code)
    {
        if (code.userId >= upgradesOfUsers.Count)
        {
            Debug.Log("Changed tree.");
            return;
        }
        Debug.LogError("TestableDestroyableMono if upgrades work");

        if (code.context == "SelectUser")
        {
            Debug.Log("SelectUser :: "+activeUser);
            activeUser = code.userId;
            title.text = "User" + (activeUser + 1);
        }

        if (code.context == "ClickUpgrade")
        {
            Debug.Log("ClickUpgrade :: on "+activeUser+ " upId: "+code.data.upgradeId);
            upgradesOfUsers[activeUser].Increase(code.data);
        }

        buttons[0].text = ""+upgradesOfUsers[activeUser].attack;
        buttons[1].text = ""+upgradesOfUsers[activeUser].health;
        buttons[2].text = ""+upgradesOfUsers[activeUser].speed;

    }

    public void SetUser(int userId)
    {
        activeUser = userId;
    }
}
