using UnityEngine;

public class ExpandedStats:MonoBehaviour, ISetupUnity
{
    public MonoUserMods speedMod;
    public MonoUserMods attackMod;

    public float BonusSpeed => enabled && speedMod ? speedMod.GetSum() : 0;
    public float BonusAttack => enabled && attackMod ? attackMod.GetSum() : 0;

    public bool UnitySetup()
    {
        if (speedMod == null)
        {
            Debug.Log("automatically setting up speed mod as child.");
            GameObject go = new GameObject("Speed mods");
            speedMod = go.AddComponent<MonoUserMods>();
            go.transform.parent = transform;
        }
        if (attackMod == null)
        {
            Debug.Log("automatically setting up attack mod as child.");
            GameObject go = new GameObject("Attack mods");
            attackMod = go.AddComponent<MonoUserMods>();
            go.transform.parent = transform;
        }
        return speedMod != null && attackMod != null;
    }
}
