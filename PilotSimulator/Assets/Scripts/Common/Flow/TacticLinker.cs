﻿using UnityEngine;

[System.Serializable]
public class TacticLinker
{

    [Tooltip("Connect tactics on selected transform to selected command.")]
    public TacticGroup tactic;

    public void ConnectTactics(Transform t)
    {
        if (tactic == null) return;

        TacticallyConnected spawnedTactics = t.gameObject.GetComponentInChildren<TacticallyConnected>();
        if (spawnedTactics == null)
        {
            Debug.Log("Couldn't find tactic on spawned.");
        }
        else
        {
            tactic.ConnectUnit(spawnedTactics);
        }
    }
}
