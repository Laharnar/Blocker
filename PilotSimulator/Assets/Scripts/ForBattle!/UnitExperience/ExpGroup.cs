using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpGroup:MonoBehaviour
{

    [SerializeField] IntVarValue exp;

    public void ConnectToChild(Transform t)
    {
        t.GetComponentInChildren<ExpCollector>().Connect(this);
        t.GetComponentInChildren<ExpCollector>().Connect(this);
    }

    public void Increase(ExpGainArgs expItem)
    {
        exp.Value += expItem.intValue;
    }

    public int GetExp(int alliance)
    {
        return exp.Value;
    }

    internal void Decrease(ExpGainArgs expItem)
    {
        exp.Value -= expItem.intValue;
    }
}