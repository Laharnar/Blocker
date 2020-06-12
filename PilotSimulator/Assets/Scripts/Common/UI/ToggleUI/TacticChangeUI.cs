using System.Collections.Generic;
using UnityEngine;

public class TacticChangeUI:MonoBehaviour
{
    [SerializeField] TacticGroup activeUser;
    [SerializeField] List<TacticGroup> additionalUsers = new List<TacticGroup>();

    public int displayedTactic;// displayed tactic can be different from active tactic
    // when unit is stunned or something

    public void ChangeTacticByUI(int id)
    {
        activeUser.ChangeTacticAndActivate(id);
        displayedTactic = id;

        for (int i = 0; i < additionalUsers.Count; i++)
        {
            if (additionalUsers[i])
            {
                additionalUsers[i].ChangeTacticAndActivate(id);
            }
        }
    }

    public void ChangeUser(TacticGroup user)
    {
        activeUser = user;
    }
}
