using System;
using System.Collections.Generic;
using UnityEngine;

public class TickRunner: MonoBehaviour
{
    static TickRunner instance;
    public BoolVarValue run = new BoolVarValue() { defaultValue = true };

    public static TickRunner GetInstance() { 
        if (instance == null)
        {
            instance = new GameObject("[Auto-singleton]Tick runner").AddComponent<TickRunner>();
        }
        return instance;
    }

    Dictionary<MonoBehaviour, ITickable> existing = new Dictionary<MonoBehaviour, ITickable>();


    private void Update()
    {
        if (!run.Value) return;
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
    internal static void Disconnection(CombatScript combatScript)
    {
        if (GetInstance().existing.ContainsKey(combatScript))
        {
            GetInstance().existing.Remove(combatScript);
        }
    }
}
