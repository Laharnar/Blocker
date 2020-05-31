using UnityEngine;

public class TacticChangeUI:MonoBehaviour
{
    [SerializeField] TacticsCommand activeUser;

    public int displayedTactic;// displayed tactic can be different from active tactic
    // when unit is stunned or something

    public void ChangeTacticByUI(int id)
    {
        activeUser.ChangeTacticAndActivate(id);
        displayedTactic = id;
    }

    public void ChangeUser(TacticsCommand user)
    {
        activeUser = user;
    }
}
