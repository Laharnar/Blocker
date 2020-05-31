using UnityEngine;

[System.Serializable]
public class TacticLinker
{

    [Tooltip("Connect tactics on selected transform to selected command.")]
    public TacticsCommand tactic;

    public void ConnectTactics(Transform t)
    {
        if (tactic == null) return;

        TacticalUnit spawnedTactics = t.gameObject.GetComponentInChildren<TacticalUnit>();
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
