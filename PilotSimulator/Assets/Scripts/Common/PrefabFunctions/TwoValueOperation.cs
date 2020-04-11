using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Operators/TwoValueOperation")]
public class TwoValueOperation:ScriptableObject {

    public bool negateA=false;
    public bool negateB=false;
    public MultiTypeValue a;
    public Operator op = Operator.Add;
    public MultiTypeValue b;
    public MultiTypeValue result;

    public UnityEvent OnRun;

    public void Add()
    {
        Debug.Log("Obsolete call."+name);
        Add2();
    }

    void Add2()
    {
        float aval = a.Value;
        float bval = b.Value;
        aval = NegationHandler(aval, true);
        bval = NegationHandler(bval, true);
        result.Value = OpHandler(op, aval, bval);
    }

    float NegationHandler(float val, bool negated)
    {
        if (negateA) return -val;
        else return val;
    }

    float OpHandler(Operator op, float a, float b)
    {
        switch (op)
        {
            case Operator.Add:
                return a + b;
            case Operator.Subtract:
                return a - b;
            case Operator.Multiply:
                return a * b;
            case Operator.Divide:
                return a / b;
            default:
                throw new System.NotImplementedException("Operator isn't defined"+op);
        }
    }

    public enum Operator {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    public string OpAsString() {
        switch (op)
        {
            case Operator.Add:
                return "+";
            case Operator.Subtract:
                return "-";
            case Operator.Multiply:
                return "*";
            case Operator.Divide:
                return "/";
            default:
                throw new System.NotImplementedException("Operator isn't defined"+op);
        }
    }

    // For event system.
    public void Run()
    {
        Add2();

        OnRun?.Invoke();
    }
}
