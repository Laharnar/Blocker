using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsList : PlaceHolderView
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
        if(code.userId!= -1)
            activeUser = code.userId;
        title.text = "User" + (code.userId+1);
        upgradesOfUsers[code.userId].Increase(code.data);

        buttons[0].text = ""+upgradesOfUsers[code.userId].attack;
        buttons[1].text = ""+upgradesOfUsers[code.userId].health;
        buttons[2].text = ""+upgradesOfUsers[code.userId].speed;
    }
}
