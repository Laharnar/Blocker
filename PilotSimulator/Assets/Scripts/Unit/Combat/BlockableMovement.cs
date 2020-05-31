using System;
using UnityEngine;

public class BlockableMovement:MonoBehaviour
{
    // Offers optional expansion for movement control to other systems.

    [SerializeField] PositionRotation movement;
    [Header("|Realtime|")]
    [SerializeField] CombatController blockedBy;
    [SerializeField] public bool isBlocked = false;

    private void Update()
    {
        // When source was destroyed, unit can get unlocked.
        if(blockedBy == null)
        {
            ForceUnlock();
        }
    }

    private void ForceUnlock()
    {
        movement.enabled = true;
        isBlocked = false;
    }

    internal void LockMovement(CombatController blocker)
    {
        movement.enabled = false;
        blockedBy = blocker;
        isBlocked = true; 
    }

    internal void UnlockMovement(CombatController blocker)
    {
        if (blocker == blockedBy || blockedBy == null)
        {
            movement.enabled = true;
            isBlocked = false;
        }
    }
}
