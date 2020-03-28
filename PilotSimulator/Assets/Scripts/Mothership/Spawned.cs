using UnityEngine;

public class Spawned:MonoBehaviour {
    public ResearchSender researchSender;

    public void SetCluster(ResearchCluster rc)
    {
        researchSender.cluster = rc;
    }
}
