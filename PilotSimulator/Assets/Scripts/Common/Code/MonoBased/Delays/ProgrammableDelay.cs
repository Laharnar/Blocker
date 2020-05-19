using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class ProgrammableDelay:MonoBehaviour {

    public UnityEvent onStart;

    public IntVarValue activeDelay;
    public FloatVarRef[] delays;
    public ConditionGroup condition;
    public bool ignoreFirstActivation = true;
    public UnityEvent onReady;

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
            if (condition.IsTrue())
            {
                if (ignoreFirstActivation && !ignoredFirst)
                {
                    ignoredFirst = true;
                    yield return new WaitForSeconds(delays[activeDelay.Value].Value);
                    ToNextDelay();
                }
                else
                {
                    ActivateEvent();

                    yield return new WaitForSeconds(delays[activeDelay.Value].Value);
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
