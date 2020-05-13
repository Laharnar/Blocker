using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class ExpCollector : MonoBehaviour
{

    public IntVarValue groupId;
    public UnityEvent onGetExp;
    public ExpGroup expUser;
    // exp collection: random min random max, flat value. use 1 use 2. trigger function

    public int logCollectedExp;
    public int logAvaliableExp;

    private void Start()
    {
    }

    public void GainExp(int value)
    {
        GainExp(new ExpGainArgs()
        {
            groupId = groupId.Value,
            intValue = value
        }) ;
    }

    internal void Connect(ExpGroup expGroup)
    {
        expUser = expGroup;
    }

    public void GainExp(ExpGainArgs value)
    {
        logCollectedExp += value.intValue;
        logAvaliableExp += value.intValue;

        onGetExp.Invoke();
        expUser.Increase(value);

        logAvaliableExp = 0;
    }
}
