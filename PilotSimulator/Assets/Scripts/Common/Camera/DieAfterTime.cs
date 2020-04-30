using UnityEngine;

public class DieAfterTime:MonoBehaviour
{
    public bool beginOnStart = true;
    public FloatVarRef dieAfterTime;
    private float startTime;

    void Start()
    {
        if (beginOnStart)
        {
            StartCounting();
        }
    }
    public void StartCounting()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime > dieAfterTime.Value)
        {
            Destroy(gameObject);
        }
    }
}
