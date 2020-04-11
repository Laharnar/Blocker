using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProgrammableDelay:MonoBehaviour {

    public FloatVarRef[] delays;
    public int activeDelay;
    public UnityEvent onReady;
    public ConditionGroup condition;

    private void Start()
    {
        StartCoroutine(RunDelays());
    }

    protected virtual IEnumerator RunDelays()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            if (condition.IsTrue())
            {
                ActivateEvent();
                yield return new WaitForSeconds(delays[activeDelay].Value);
                ToNextDelay();
            }
            yield return null;
        }
    }

    protected void ActivateEvent()
    {
        try
        {
            onReady?.Invoke();
        }
        catch (Exception e)
        {
            Debug.Log("Could crash spawner coroutine.");
            Debug.LogException(e);
        }
    }

    protected void ToNextDelay()
    {
        activeDelay = (activeDelay + 1) % delays.Length;
    }
}
