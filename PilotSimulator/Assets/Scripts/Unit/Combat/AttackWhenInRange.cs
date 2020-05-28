using UnityEngine;

public class AttackWhenInRange: ITactic
{
    public Transform target;
    public Transform source;
    public float range;
    public float userOffForSeconds;
    public TacticResult SuggestedResult { get; set; }
    [Space]
    public bool logNoTarget = true;
    bool used = false;

    public AttackWhenInRange()
    {
        SuggestedResult = new TacticResult();
    }
    public void Simulate()
    {
        if (used)
        {
            logNoTarget = target == null;
            RealtimeTester.Assert(source != null, this, "Source isn't assigned to tactic of type AttackWhenInRange.");
            if (target)
            {
                if (Vector3.Distance(target.position, source.position) < range)
                {
                    SuggestedResult.ChangeGoal("Attack", Time.time);
                }
                else
                {
                    SuggestedResult.ChangeGoal("Other", Time.time);
                }
            }
            else SuggestedResult.ChangeGoal("NoTarget", Time.time);
        }
    }

    public void Activate()
    {
        used = true;
    }

    public void Deactivate()
    {
        used = false;
    }
}
