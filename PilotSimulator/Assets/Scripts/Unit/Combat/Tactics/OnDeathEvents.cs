using UnityEngine;
using UnityEngine.Events;

public class OnDeathEvents: MonoBehaviour
{
    public UnityEvent onStart;
    public UnityEvent onDeath;

    public bool manualTrigger = false;

    private void Start()
    {
        onStart.Invoke();
    }

    private void Update()
    {
        if (manualTrigger)
        {
            manualTrigger = false;
            OnDeath();
        }
    }

    public void OnDeath()
    {
        onDeath.Invoke();
    }
}
