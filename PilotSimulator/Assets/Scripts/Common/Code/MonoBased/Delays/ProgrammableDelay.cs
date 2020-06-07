using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class ProgrammableDelay:MonoBehaviour {
    // tude use delays directly from other scripts, use

    public UnityEvent onStart;

    public IntVarValue activeDelay;
    public FloatVarRef[] delays;
    public ConditionGroup condition;
    public bool ignoreFirstActivation = true;
    public UnityEvent onReady;
    bool isReady = false;

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
            if (enabled && condition.IsTrue())
            {
                if (ignoreFirstActivation && !ignoredFirst)
                {
                    ignoredFirst = true;
                    yield return new WaitForSeconds(delays[activeDelay.Value].Value);
                    ToNextDelay();
                }
                else
                {
                    yield return StartCoroutine(RunOnce());
                }
            }
            yield return null;
        }
    }

    private IEnumerator RunOnce()
    {
        isReady = false;
        ActivateEvent();

        yield return new WaitForSeconds(delays[activeDelay.Value].Value);
        ToNextDelay();
        isReady = true;
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
        activeDelay.Value = (activeDelay.Value + 1) % delays.Length;
    }
}
