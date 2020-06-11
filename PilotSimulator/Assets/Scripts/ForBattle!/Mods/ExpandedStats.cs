using UnityEngine;


// combat accesible stats
public class ExpandedStats:MonoBehaviour
{
    public MonoConnection speedStatMod;
    public MonoConnection attackStatMod;
    public float BonusSpeed => gameObject.activeSelf && attackStatMod.StatGetter != null ? speedStatMod.StatGetter.GetSum() : 0;
    public float BonusAttack => gameObject.activeSelf && attackStatMod.StatGetter != null ? attackStatMod.StatGetter.GetSum() : 0;


}
