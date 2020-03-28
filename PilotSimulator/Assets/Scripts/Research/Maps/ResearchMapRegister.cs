using UnityEngine;

public class ResearchMapRegister:MonoBehaviour {
    // Register this cluster to ui. Required because there can be multiple clusters.
    private void Awake()
    {
        GameObject.FindObjectOfType<ResearchMapsDisplay>().SetSourceCluster(GetComponent<ResearchCluster>());
    }

}
