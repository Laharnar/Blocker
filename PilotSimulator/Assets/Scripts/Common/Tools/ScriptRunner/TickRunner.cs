using System;
using System.Collections.Generic;
using UnityEngine;

public class TickRunner: MonoBehaviour, ISetupUnity
{
    static TickRunner instance;
    public BoolVarValue run = new BoolVarValue() { defaultValue = true };

    Dictionary<MonoBehaviour, ITickable> existing = new Dictionary<MonoBehaviour, ITickable>();

    static bool quittingGame = false;

    public static TickRunner GetInstance() { 
        if (instance == null)
        {
            // time condition fixes issue where instance seems to get created when stopping game.
            if (Application.isPlaying && !quittingGame)
            {
                Debug.Log("Creating custom tick runner.");
                instance = new GameObject("[Auto-singleton]Tick runner").AddComponent<TickRunner>();
            }
        }
        return instance;
    }
    void OnApplicationQuit()
    {
        quittingGame = true;
    }

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

    public bool UnitySetup()
    {
        GetInstance();
        return true;
    }
}
