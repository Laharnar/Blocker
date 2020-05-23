using UnityEngine;
using UnityEngine.Events;

public class TacticUser:MonoBehaviour
{
    public Tactics avaliable;
    public bool useTactic = true;
    public int activeTactic;
    public int displayTactic;

    public UnityEvent onSimulation;

    public void Activate()
    {
        if (useTactic)
        {
            avaliable.tactics[activeTactic].Simulate();
            onSimulation.Invoke();
        }
    }

    public void SetTacticByUI(int i)
    {
        displayTactic = i;

        if(useTactic)
            activeTactic = i;
    }

    public void ChangeUse(bool newUse)
    {
        if (newUse!= useTactic)
        {
            useTactic = newUse;
        }
    }

    public void Log()
    {
        Debug.Log("Logged tactic");
    }
}
