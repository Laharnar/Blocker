using UnityEngine;

public class EnemyWorth:MonoBehaviour {
    public int worthOnDeath;
    public IntVarValue collected;



    public void AddVSPToAnyCluster()
    {
        collected.Value += worthOnDeath;
    }
}