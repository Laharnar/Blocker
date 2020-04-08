using System.Collections.Generic;
using UnityEngine;

// todo: remove
public class GlobalStorage:MonoBehaviour {
    static GlobalStorage singleton;
    public class PairStorage<T> : Dictionary<Transform, T> {

    }
    PairStorage<Alliance> known = new PairStorage<Alliance>();
    PairStorage<DamageSender> dmgs = new PairStorage<DamageSender>();
    PairStorage<DamageReciever> dmgr = new PairStorage<DamageReciever>();

    public static void RegisterGlobally(Transform t, Alliance alliance)
    {
        singleton.known.Add(t, alliance);
    }

    public static void RegisterGlobally(Transform t, DamageSender sender)
    {
        singleton.dmgs.Add(t, sender);
    }

    public static void RegisterGlobally(Transform t, DamageReciever recv)
    {
        singleton.dmgr.Add(t, recv);
    }

    internal static void GlobalOnHitHandler(Transform from, Transform to)
    {
        singleton.dmgs[from].OnHit(singleton.dmgr[to]);
    }

    internal static bool AlliancesMatch(Transform from, Transform to)
    {
        return singleton.known[from].thisAlliance == singleton.known[to].thisAlliance;
    }

    internal static bool TransformKeysExist(params MonoBehaviour[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (!singleton.known.ContainsKey(items[i].transform))
            {
                Debug.LogError("Global storage doesn't contain transform signature "+ items[i].transform+". Make sure it's registred.");
                return false;
            }
        }
        return true;
    }
}
