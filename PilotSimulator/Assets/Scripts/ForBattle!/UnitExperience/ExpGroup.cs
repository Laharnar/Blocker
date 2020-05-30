using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpGroup: MonoBehaviour, IUIValue
{

    [SerializeField] IntVarValue exp;

    public bool IsChanged { get; set; }

    public void ConnectExpToChild(Transform t)
    {
        t.GetComponentInChildren<ExpCollector>().Connect(this);
        t.GetComponentInChildren<ExpCollector>().Connect(this);
    }

    public void Increase(ExpGainArgs expItem)
    {
        exp.Value += expItem.intValue;
        IsChanged = true;
    }

    public int GetExp()
    {
        return exp.Value;
    }

    internal void Decrease(ExpGainArgs expItem)
    {
        Debug.Log("Subtracted "+expItem.intValue);
        exp.Value -= expItem.intValue;
        IsChanged = true;
    }

    public string GetContent()
    {
        return "" + GetExp();
    }
}