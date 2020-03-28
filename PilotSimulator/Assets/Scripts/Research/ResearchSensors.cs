using UnityEngine;

public class ResearchSensors:MonoBehaviour {
    public bool saveLastImportantPointOnStart;
    public ResearchResult results;
    public bool logGather = true;

    private void Start()
    {
        results = new ResearchResult();
        if(saveLastImportantPointOnStart)
            results.lastImportantPoint = transform.position;
    }

    public void Gather()
    {
        if (logGather)
        {
            Debug.Log("gathering research", this);
        }
        results.recordedRelativePosition = transform.position - results.lastImportantPoint;
    }

}
