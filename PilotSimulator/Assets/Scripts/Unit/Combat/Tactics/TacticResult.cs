using UnityEngine;

public class TacticResult
{
    public float timeUpdated;
    public string code;
    public bool spent;

    public TacticResult()
    {
        code = "init";
        spent = true;
        timeUpdated = Time.time;
    }

    public void ChangeGoal(string c, float time)
    {
        code = c;
        spent = true;
        timeUpdated = time;
    }
}
