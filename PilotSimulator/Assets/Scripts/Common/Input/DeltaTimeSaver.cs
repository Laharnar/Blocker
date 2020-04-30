using UnityEngine;

public class DeltaTimeSaver : MonoBehaviour
{

    public FloatVar deltaTime;

    private void Update()
    {
        deltaTime.Value = Time.deltaTime;
    }
}
