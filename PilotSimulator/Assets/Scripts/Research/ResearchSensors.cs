using UnityEngine;

public class ResearchSensors:MonoBehaviour {
    public bool saveLastImportantPointOnStart;

    public ResearchUnitArgs results;

    public bool logGather = true;

    private void Start()
    {
        results = new ResearchUnitArgs();
        if(saveLastImportantPointOnStart)
            results.lastImportantPoint = transform.position;
    }

    // Events
    public void Gather()
    {
        if (logGather)
        {
            Debug.Log("gathering research", this);
        }
        results.recordedRelativePosition = transform.position - results.lastImportantPoint;
    }

}
