using System;
using System.Collections.Generic;
using UnityEngine;


public class Linker : MonoBehaviour, ITestable
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
        RealtimeTester.Assert(linked, this, "Recieved linked is null.");
    }

    internal void DualConnect(ExpCollector expCollector)
    {
        throw new NotImplementedException();
    }

    internal void Disconnect(Linked linked)
    {
        targets.Remove(linked);
    }

    public void TestInitialState()
    {
    }
}
