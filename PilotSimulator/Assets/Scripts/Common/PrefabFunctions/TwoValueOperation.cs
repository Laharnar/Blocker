using UnityEngine;

[CreateAssetMenu(menuName = "Operators/Addition")]
public class TwoValueOperation:ScriptableObject {

    
    public bool negateA=false;
    public bool negateB=false;
    public IntVarValue a;
    public Operator op = Operator.Add;
    public IntVarValue b;
    public IntVarValue result;
    

    // For event system.
    public void Add()
    {
        int aval = a.Value;
        int bval = b.Value;
        aval = NegationHandler(aval, true);
        bval = NegationHandler(bval, true);
        result.Value = OpHandler(op, aval, bval);
    }

    int NegationHandler(int val, bool negated)
    {
        if (negateA) return -val;
        else return val;
    }

    int OpHandler(Operator op, int a, int b)
    {
        switch (op)
        {
            case Operator.Add:
                return a + b;
                break;
            case Operator.Subtract:
                return a - b;
                break;
            case Operator.Multiply:
                return a * b;
                break;
            case Operator.Divide:
                return a / b;
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    public enum Operator {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public void Run()
    {
        Add();
    }
}
