using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProgrammableItemDelay : MonoBehaviour
{

    public UnityEvent onStart;

    public IntVarValue activeDelay;
    public DelayedConditionEvent[] events;
    public bool ignoreFirstActivation = true;
    bool ignoredFirst = false;

    private void Start()
    {
        if(enabled)
            onStart.Invoke();
        StartCoroutine(RunDelays());
    }

    protected virtual IEnumerator RunDelays()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            if (enabled)
            {
                if (events[activeDelay.Value].condition.IsTrue())
                {
                    yield return StartCoroutine(events[activeDelay.Value].delay.Delay(0));
                    ActivateEvent();
                    ToNextDelay();
                }
            }
            yield return null;
        }
    }

    protected void ActivateEvent()
    {
        try
        {
            events[activeDelay.Value].onReady?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError("Could crash spawner coroutine.");
            Debug.LogException(e);
        }
    }

    protected void ToNextDelay()
    {
        activeDelay.Value = (activeDelay.Value + 1) % events.Length;
    }
}
