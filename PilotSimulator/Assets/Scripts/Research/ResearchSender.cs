using UnityEngine;

public class ResearchSender:MonoBehaviour {

    public MonoBehaviourVar researchCluster;
    public ResearchSensors researchSensors;

    // Events
    public void Deliver()
    {
        if (ValuesValid())
        {
            ResearchCluster rc = researchCluster.GetValueAs<ResearchCluster>();
            rc.RegisterNewResearch(researchSensors.results);
        }
    }

    bool ValuesValid()
    {
        if (researchCluster == null)
        {
            Debug.LogError("Cluster value is null " + gameObject.name, gameObject);
            return false;
        }
        else if (researchSensors == null)
        {
            Debug.LogError("Sensors value is null " + gameObject.name, gameObject);
            return false;
        }
        return true;
    }
}
