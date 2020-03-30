using UnityEngine;

public class WASDInputSaver:MonoBehaviour {

    public FloatVar horizontal;
    public FloatVar vertical;

    private void Update()
    {
        horizontal.value = Input.GetAxis("Horizontal");
        vertical.value = Input.GetAxis("Vertical");
    }
}
