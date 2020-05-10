using System;
using UnityEngine.Events;

[Serializable]
public class DelayedConditionEvent
{
    public StagedDelay delay = new StagedDelay() { stages = new bool[3] };
    public ConditionGroup condition;
    public UnityEvent onReady;
}
