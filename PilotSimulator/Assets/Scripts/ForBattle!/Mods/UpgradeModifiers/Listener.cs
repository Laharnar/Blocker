using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class Listener
{
    [SerializeField] List<ListenersObserver> observers;
    [SerializeField] List<string> observerNames;
    public int registeredCount = 0;
    public bool reset = false;

    public Listener()
    {
        observers = new List<ListenersObserver>();
        observerNames = new List<string>();
        registeredCount = 0;
    }

    public void RegisterObserver(ValueChangeNotifiable item)
    {
        if (reset)
        {
            observers.Clear();
            reset = false;
        }
        observers.Add(item);
        observerNames.Add(item.name);
        registeredCount = observers.Count;
    }

    public void Notify(IModData mods)
    {
        if (reset)
        {
            observers.Clear();
            observerNames.Clear();
            reset = false;
        }
        for (int i = 0; i < observers.Count; i++)
        {
            if(observerNames[i] != null)
                observers[i].Notified(mods);
        }
    }

    internal void UnregisterObserver(ValueChangeNotifiable item)
    {
        if (reset)
        {
            observers.Clear();
            observerNames.Clear();
            reset = false;
        }
        observers.Remove(item);
        observerNames.Remove(item.name);
        registeredCount = observers.Count;
    }
}
