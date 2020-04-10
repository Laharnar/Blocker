using UnityEngine;


[System.Serializable]
public class ConditionGroup {
    public Condition[] conditions;
    public int op;// 0: and, 1: or
    public bool valueFor0Len = true;
    public bool skip = false;

    public bool IsTrue()
    {
        if (skip)
            return valueFor0Len;
        if (op == 0)
        {
            return And();
        }
        else if( op == 1)
        {
            return Or();
        }
        else
        {
            throw new System.NotImplementedException("Op number isn't implemented. "+op);
        }
    }

    private bool Or()
    {
        if (conditions.Length == 0)
        {
            return valueFor0Len;
        }
        for (int i = 0; i < conditions.Length; i++)
        {
            if (conditions[i].IsTrue())
                return true;
        }
        return false;
    }

    private bool And()
    {
        if (conditions.Length == 0)
        {
            return valueFor0Len;
        }
        for (int i = 0; i < conditions.Length; i++)
        {
            if (conditions[i].IsFalse())
                return false;
        }
        return true;
    }
}
