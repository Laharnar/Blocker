using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Operators/TwoValueOperation")]
public class TwoValueOperation:ScriptableObject {

    public bool negateA=false;
    public bool negateB=false;
    public bool keepSignA = false;
    public bool keepSignB = false;
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
        float signa = Math.Sign(a);
        float signb = Math.Sign(b);
        float result = 0;
        switch (op)
        {
            case Operator.Add:
                result = a + b;
                break;
            case Operator.Subtract:
                result = a - b;
                break;
            case Operator.Multiply:
                result = a * b;
                break;
            case Operator.Divide:
                if (b == 0)
                    result = 0;
                else result = a / b;
                break;
            default:
                throw new System.NotImplementedException("Operator isn't defined"+op);
        }
        if (keepSignA)
        {
            result*= signa;
        }

        if (keepSignB)
        {
            result *= signb;
        }
        return result;
    }

    public enum Operator {
        Add,
        Subtract,
        Multiply,
        Divide,
        SignA
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
