using UnityEngine;

public class CircleRangeStatMod : StatMods, ISetupUnity
{
    public CircleCollider2D circleCollider;
    bool savedStartupValue;
    float startupRadius;

    public bool UnitySetup()
    {
        RealtimeTester.Assert(circleCollider != null, this, "Collider isn't connected.");
        if (circleCollider && !Application.isPlaying)
        {
            startupRadius = circleCollider.radius;
            return true;
        }
        return false;
    }

    protected override void OnAddedMod()
    {
        SaveValueOnceInStartupOrEditorMode();
        circleCollider.radius = startupRadius + GetSum();
    }

    private void SaveValueOnceInStartupOrEditorMode()
    {
        if (!savedStartupValue || !Application.isPlaying)
        {
            startupRadius = circleCollider.radius;
        }
        if (Application.isPlaying) savedStartupValue = true;
    }
}
