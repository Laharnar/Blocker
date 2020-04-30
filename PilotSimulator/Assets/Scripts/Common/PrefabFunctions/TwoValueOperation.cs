using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OperatorTransitional
{
    public TwoValueOperation.Operator op;
    public TwoValueOperation.Operator Value => op;
}

[CreateAssetMenu(menuName = "Operators/TwoValueOperation")]
public class TwoValueOperation:ScriptableObject {

    public bool negateA=false;
    public bool negateB=false;
    public bool keepSignA = false;
    public bool keepSignB = false;
    public MultiTypeValue a;
    public Operator op = Operator.Add;
    public OperatorTransitional opTmp;

    public MultiTypeValue b;
    public MultiTypeValue result;

    public UnityEvent OnRun;

    public void Add()
    {
        Debug.LogError("Obsolete call."+name+" Use Run");
        Add2();
    }

    void Add2()
    {
        float aval = a.Value;
        float bval = b.Value;
        aval = NegationHandler(aval, true);
        bval = NegationHandler(bval, true);
        float operated = Operate(opTmp.Value, aval, bval);
        float signed = Signs(a.Value, keepSignA,
                        Signs(b.Value, keepSignB,
                            operated));
        result.Value = signed;
    }

    float NegationHandler(float val, bool negated)
    {
        if (negateA) return -val;
        else return val;
    }

    public static float Operate(Operator op, float a, float b)
    {
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
            case Operator.SetToA:
                result = a;
                break;
            case Operator.SetToB:
                result = b;
                break;
            default:
                throw new System.NotImplementedException("Operator isn't defined"+op);
        }
        
        return result;
    }

    float Signs(float s1, bool keep, float value)
    {
        if (keep)
        {
            value *= Math.Sign(s1);
        }
        return value;
    }

    public enum Operator {
        Add,
        Subtract,
        Multiply,
        Divide,
        SignA,
        SetToA,
        SetToB
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
            case Operator.SetToA:
                return "=a=" ;
            case Operator.SetToB:
                return "=b=";
            default:
                throw new System.NotImplementedException("Operator isn't defined"+op);
        }
    }

    // For event system.
    public void Run()
    {
        opTmp.op = op;
        Add2();

        OnRun?.Invoke();
    }
}
