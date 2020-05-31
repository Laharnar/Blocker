using System;
using UnityEngine;

public class BlockableMovement:MonoBehaviour
{
    [SerializeField] PositionRotation movement;
    [Header("|Realtime|")]
    [SerializeField] CombatController blockedBy;
    [SerializeField] public bool isBlocked = false;

    private void Update()
    {
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
