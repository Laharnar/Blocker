using System.Collections.Generic;
using UnityEngine;

public class TickRunner: MonoBehaviour
{
    static TickRunner instance;

    public static TickRunner GetInstance() { 
        if (instance == null)
        {
            instance = new GameObject().AddComponent<TickRunner>();
        }
        return instance;
    }

    Dictionary<MonoBehaviour, ITickable> existing = new Dictionary<MonoBehaviour, ITickable>();

    private void Update()
    {
        foreach (var item in existing)
        {
            item.Value.Tick();
        }
    }

    internal static void EnsureConnection(CombatScript combatScript)
    {
        if (!GetInstance().existing.ContainsKey(combatScript))
        {
            GetInstance().existing.Add(combatScript, combatScript);
        }
    }
}
