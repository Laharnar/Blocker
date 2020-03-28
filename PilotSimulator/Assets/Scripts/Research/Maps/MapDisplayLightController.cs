using UnityEngine;

public class MapDisplayLightController : DisplayControllerBase {

    public Light lights;
    public Color[] colorMode;
    public int activeMode;

    [ContextMenu("TestActiveMode")]
    public void SetToActiveMode()
    {
        SetMode(activeMode);
    }

    public void SetMode(int i)
    {
        activeMode = i;
        if (i<colorMode.Length)
        {
            lights.color = colorMode[i];
        }
    }

    internal override void DisableVisuals()
    {
        SetMode(0);
    }

    internal override void EnableVisuals()
    {
        SetMode(1);
    }
}