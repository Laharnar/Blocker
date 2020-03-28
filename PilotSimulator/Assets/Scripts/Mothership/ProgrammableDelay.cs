using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProgrammableDelay:MonoBehaviour {

    public FloatVarRef[] delays;
    public int activeDelay;
    public UnityEvent onReady;

    private void Start()
    {
        StartCoroutine(RunDelays());
    }

    IEnumerator RunDelays()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            ActivateEvent();
            yield return new WaitForSeconds(delays[activeDelay].Value);
            ToNextDelay();
            yield return null;
        }
    }

    private void ActivateEvent()
    {
        onReady?.Invoke();
    }

    private void ToNextDelay()
    {
        activeDelay = (activeDelay + 1) % delays.Length;
    }
}
