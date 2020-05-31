using UnityEngine;

public class TacticalUnit:MonoGroup
{
    // Contains connection for single unit to tactics on unit, commanding officer and target bases.

    [Header("| Realtime |")]
    [SerializeField] int activatedId = 0;
    [Header("| Linked |")]
    [SerializeField] internal TacticsCommand officer;
    [SerializeField] internal Transform AllyBaseToDefend;
    [SerializeField] internal CombatUser EnemyBossToAttack;

    public void Activate(int i)
    {
        DeactivateAll();
        if (i < connections.Count)
        {
            activatedId = i;
            connections[i].MonoTactic.Activate();
        }
        else Debug.LogError("Not enough tactics. Index out of range when activating tactic.", this);
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
