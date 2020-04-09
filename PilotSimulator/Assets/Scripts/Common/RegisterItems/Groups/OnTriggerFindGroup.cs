using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerFindGroup:GroupOfRegistered {

    private List<Register> all { get => Registred; }
    public UnityEvent OnStart;
    public UnityEvent OnFindNew;
    public UnityEvent OnLostOne;
    public UnityEvent OnLostAll;

    private void Start()
    {
        OnStart?.Invoke();
    }

    private void Update()
    {
        // Null items from destroy calls have to be removed in this script, because registration also happens here.
        LoseNulls();
    }

    public void TurnOff()
    {
        LoseAll();
    }

    private void LoseAll()
    {
        for (int i = all.Count - 1; i >= 0; i--)
        {
            Lost(all[i]);
        }
    }

    private void LoseNulls()
    {

        for (int i = all.Count - 1; i >= 0; i--)
        {
            if (all[i] == null)
            {
                Lost(all[i]);
            }
        }
    }

    public void FindNew(Register detectable)
    {
        //absorbables.Add(detectable);
        Register(detectable);
        OnFindNew.Invoke();
    }

    public void Lost(Register detectable)
    {
        if (Unregister(detectable))
        {
        }
        OnLostOne.Invoke();

        if (all.Count == 0)
            OnLostAll.Invoke();
    }

    protected void OnTriggerEnter(Collider other)
    {
        Register o = other.gameObject.GetComponent<Register>();
        if (o && o != this)
        {
            FindNew(o);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Register o = other.gameObject.GetComponent<Register>();
        if (o && o != this)
        {
            Lost(o);
        }
    }
}
