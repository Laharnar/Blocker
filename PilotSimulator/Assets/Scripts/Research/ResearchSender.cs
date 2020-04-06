using UnityEngine;

public class ResearchSender:MonoBehaviour {

    public MonoBehaviourVar researchCluster;
    public ResearchSensors researchSensors;

    public void Deliver()
    {
        if (researchCluster == null)
        {
            Debug.LogError("Cluster value is null " + gameObject.name, gameObject);
        }
        else if (researchSensors == null)
        {
            Debug.LogError("Sensors value is null "+ gameObject.name, gameObject);
        }
        else
        {
            ((ResearchCluster)researchCluster.Value).RegisterNewResearch(researchSensors.results);
        }
    }
}
