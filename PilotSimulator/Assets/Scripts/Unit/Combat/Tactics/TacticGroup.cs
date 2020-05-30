using UnityEngine;

public class TacticGroup:MonoGroup
{
    public int activatedId = 0;
    public TacticsCommand officer;

    public void Activate(int i)
    {
        DeactivateAll();
        if (i < connections.Count)
        {
            activatedId = i;
            connections[i].MonoTactic.Activate();
        }
        else Debug.LogError("Index out of range when activating tactic.", this);
    }

    public void DeactivateAll()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            connections[i].MonoTactic.Deactivate();
        }
        activatedId = -1;
    }

    private void OnDestroy()
    {
        if(officer)officer.DisconnectUnitOnDestroy(this);
    }
}
