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
        onStart.Invoke();
        StartCoroutine(RunDelays());
    }

    protected virtual IEnumerator RunDelays()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            yield return StartCoroutine(events[activeDelay.Value].delay.Delay(0));
            Debug.Log("done delay 0");
            if (events[activeDelay.Value].condition.IsTrue())
            {
                yield return StartCoroutine(events[activeDelay.Value].delay.Delay(1));
            Debug.Log("done delay 1");
                ActivateEvent();
            Debug.Log("activated");
                yield return StartCoroutine(events[activeDelay.Value].delay.Delay(2));
            Debug.Log("done delay 2");
                ToNextDelay();
            Debug.Log("next");
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
