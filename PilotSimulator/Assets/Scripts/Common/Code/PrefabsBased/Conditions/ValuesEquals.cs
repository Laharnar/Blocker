using UnityEngine;

[CreateAssetMenu(menuName ="Operators/Equals")]
public class ValuesEquals:Condition {

    public MultiTypeValue requiredValue;
    public Operator op;
    public MultiTypeValue currentValue;
    public BoolVarValue state;
    [SerializeField]float lastComparedA = 0;
    [SerializeField] float lastComparedB = 0;

    public override bool IsTrue()
    {
        state.Value = EvalByOperator();
        return state.Value;
    }
    
    private bool EvalByOperator()
    {
        lastComparedA = requiredValue.Value;
        lastComparedB = currentValue.Value;
        switch (op)
        {
            case Operator.Equals:
                return requiredValue.Value == currentValue.Value;
            case Operator.Not:
                return requiredValue.Value != currentValue.Value;
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
