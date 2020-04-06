﻿using UnityEngine;

[CreateAssetMenu(menuName ="Operators/Equals")]
public class ValuesEquals:Condition {

    public IntVarValue requiredValue;
    public Operator op;
    public IntVarValue currentValue;

    public override bool IsTrue()
    {
        return EvalByOperator();
    }
    
    private bool EvalByOperator()
    {
        switch (op)
        {
            case Operator.Equals:
                return requiredValue.Value == currentValue.Value;
            case Operator.Not:
                return requiredValue.Value == currentValue.Value;
            case Operator.Less:
                return requiredValue.Value < currentValue.Value;
            case Operator.More:
                return requiredValue.Value > currentValue.Value;
            case Operator.LessOrEquals:
                return requiredValue.Value <= currentValue.Value;
            case Operator.MoreOrEquals:
                return requiredValue.Value >= currentValue.Value;
            default:
                throw new System.NotImplementedException("Operator isn't handled " + op);
        }
    }
}