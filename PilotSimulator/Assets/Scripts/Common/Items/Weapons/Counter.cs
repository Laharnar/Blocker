using UnityEngine;
using UnityEngine.Events;
public class Counter : MonoBehaviour
{
    public int max;
    public UnityEvent onStart;
    public UnityEvent onHitMax;
    public int cur = 0;

    private void Start()
    {
        onStart.Invoke();
    }

    public void ResetCurrent()
    {
        cur = 0;
    }

    public void Increase()
    {
        cur++;
        if (cur >= max)
            onHitMax.Invoke();
    }
}
