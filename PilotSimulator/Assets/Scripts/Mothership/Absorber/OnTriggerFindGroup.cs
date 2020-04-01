﻿using System;
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

    public void FindNew(Register detectable)
    {
        //absorbables.Add(detectable);
        Register(detectable);
        OnFindNew.Invoke();
    }

    public void Lost(Register detectable)
    {
        //absorbables.Remove(detectable);
        Unregister(detectable);
        OnLostOne.Invoke();

        if (all.Count == 0)
            OnLostAll.Invoke();
    }

    public Register[] GetAllCostly()
    {
        return all.ToArray();
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
