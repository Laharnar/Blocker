using UnityEngine;

public class TacticGroup:MonoGroup
{
    public void Activate(int i)
    {
        DeactivateAll();
        if (i < connections.Count)
            connections[i].MonoTactic.Activate();
        else Debug.LogError("Index out of range when activating tactic.", this);
    }

    public void DeactivateAll()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            connections[i].MonoTactic.Deactivate();
        }
    }
}
