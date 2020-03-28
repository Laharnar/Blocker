using UnityEngine;

public class ResearchSender:MonoBehaviour {

    public ResearchCluster cluster;
    public ResearchSensors sensors;

    public void Deliver()
    {
        cluster.RegisterNewResearch(sensors.results);
    }
}
