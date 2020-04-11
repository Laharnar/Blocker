using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Functions/IfElse")]
public class IfElsePrefab : ScriptableObject {
    public bool autoTrue = false;
    public bool autoFalse = false;
    public ConditionGroup conditionsToUse;

    [Header("ran first, nullable, recommended")]
    public DoPrefab onClear;
    public DoPrefab onFail;

    [Header("ran second")]
    public bool skipClear = false;
    public UnityEvent onClearCondition;
    public bool skipFail = false;
    public UnityEvent onFailCondition;

    // can also be called from other scripts or by unity event.
    public void IfThenElse()
    {
        if (autoTrue || conditionsToUse.IsTrue())
        {
            if(onClear!= null)
                onClear.DoIt();
            if (!skipClear)
                onClearCondition?.Invoke();
        }
        else
        {
            if (onFail != null)
                onFail.DoIt();
            if (!skipFail || autoFalse)
                onFailCondition?.Invoke();
        }
    }
}
