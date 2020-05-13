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
            targets[i].OverLink();
        }
    }

    internal void Connect(Linked linked)
    {
        targets.Add(linked);
        if (linked == null)
            Debug.LogError("Linker connecting to null. Probably an error.", this);
    }

    internal void DualConnect(ExpCollector expCollector)
    {
        throw new NotImplementedException();
    }

    internal void Disconnect(Linked linked)
    {
        targets.Remove(linked);
    }

}
