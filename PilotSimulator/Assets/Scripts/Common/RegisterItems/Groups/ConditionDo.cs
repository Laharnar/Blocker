using UnityEngine;
using UnityEngine.Events;

public class ConditionDo:MonoBehaviour {
    public UnityEvent preCondition;
    public ConditionGroup conditionsToUse;
    public bool skipClear = false;
    public UnityEvent onClearCondition;
    public bool skipFail = false;
    public UnityEvent onFailCondition;

    public bool useEveryFrame = true;

    private void Update()
    {
        if (useEveryFrame)
        {
            IfThenElse();
        }
    }

    // can also be called from other scripts or by unity event.
    public void IfThenElse()
    {
        preCondition?.Invoke();
        if (conditionsToUse.IsTrue())
        {
            if(!skipClear)
                onClearCondition?.Invoke();
        }
        else
        {
            if(!skipFail)
                onFailCondition?.Invoke();
        }
    }
}
