using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventItem : MonoBehaviour
{
    public UnityEvent onStart;
    public EventOption[] options;

    // Start is called before the first frame update
    void Start()
    {
        onStart?.Invoke();
    }
    public void TriggerOption(int i)
    {
        options?[i].Activate();
    }

    public void UnsafeCall(string description)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if(options[i].description == description)
            {
                options[i].evt?.Invoke();
                return;
            }
        }
        Debug.LogError("Call with description was not found "+description, this);
    }
}

[System.Serializable]
public class EventOption {
    public string description;
    public UnityEvent evt;

    public void Activate()
    {
        evt?.Invoke();
    }
}