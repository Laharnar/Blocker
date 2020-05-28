using UnityEngine;
using UnityEngine.Events;
public class UnitTactics:MonoBehaviour
{
    public TacticGroup tactics;
    public bool useTactic = true;
    public int activeTactic;
    public int displayTactic;

    public UnityEvent onSimulation;

    public void Activate()
    {
        if (!useTactic) return;

        //tactics.Activate(activeTactic).Simulate();
        onSimulation.Invoke();
    }

    public void SetTacticByUI(int i)
    {
        displayTactic = i;

        if(useTactic)
            activeTactic = i;
    }

    public void SetIsUsed(bool use)
    {
        useTactic = use;
    }

    public void Log()
    {
        Debug.Log("Logged tactic");
    }
}
