using UnityEngine;

public class EnemyWorth:MonoBehaviour {
    public int worthOnDeath;
    ResearchTree tree;



    public void AddVSPToAnyCluster()
    {
        if(tree == null)
            tree = GameObject.FindObjectOfType<ResearchTree>();
        tree.AddVSP(worthOnDeath);
    }
}