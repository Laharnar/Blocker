using System;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{
    public List<Linked> targets = new List<Linked>();

    public void Activate()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].TriggerLink();
        }
    }

    internal void Connect(Linked linked)
    {
        targets.Add(linked);
    }

    internal void Disconnect(Linked linked)
    {
        targets.Remove(linked);
    }
}
