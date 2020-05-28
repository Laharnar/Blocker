using System;
using System.Collections.Generic;
using UnityEngine;

public class MonoGroup:MonoBehaviour
{
    [SerializeField] protected List<MonoConnection> connections = new List<MonoConnection>();
    
    public void AddTactic(MonoConnection tactic)
    {
        connections.Add(tactic);
    }

    public void RemoveTactic(MonoConnection tactic)
    {
        connections.Add(tactic);
    }
}
