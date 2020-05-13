using System.Collections.Generic;
using UnityEngine;

public class ExpGroup:MonoBehaviour
{

    public List<IntVarValue> expInGroups = new List<IntVarValue>();

    public void ConnectToChild(Transform t)
    {
        t.GetComponentInChildren<ExpCollector>().Connect(this);
    }

    public void Increase(ExpGainArgs expItem)
    {
        expInGroups[expItem.groupId].Value += expItem.intValue;
    }
}