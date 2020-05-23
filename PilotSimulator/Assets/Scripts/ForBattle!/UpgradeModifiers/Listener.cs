using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Listener
{
    [SerializeField] List<Observer> observers;
    [SerializeField] List<string> observerNames;
    public int registeredCount = 0;
    public bool reset = false;

    public Listener()
    {
        observers = new List<Observer>();
        observerNames = new List<string>();
        registeredCount = 0;
    }

    public void RegisterObserver(Observer item)
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

    public void Notify(float value)
    {
        if (reset)
        {
            observers.Clear();
            observerNames.Clear();
            reset = false;
        }

        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].Notified(value);
        }
    }

    internal void UnregisterObserver(Observer item)
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
