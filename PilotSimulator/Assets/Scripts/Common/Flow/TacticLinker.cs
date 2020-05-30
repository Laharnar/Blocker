using UnityEngine;

[System.Serializable]
public class TacticLinker
{

    [Tooltip("Connect tactics on selected transform to selected command.")]
    public TacticsCommand tactic;

    public void ConnectTactics(Transform t)
    {
        if (tactic == null) return;

        TacticGroup spawnedTactics = t.gameObject.GetComponentInChildren<TacticGroup>();
        if (spawnedTactics == null)
        {
            Debug.Log("Couldn't find tactic on spawned.");
        }
        else
        {
            tactic.AddUnit(spawnedTactics);
        }
    }
}
