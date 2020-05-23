using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMods: Observer
{
    [SerializeField] float logSum = 0;
    protected IUpgradeMods upgradeMods;
    [SerializeField] bool init = false;

    public virtual void SetMods(IUpgradeMods userMods)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.Log(name+ " is disabled. Not setting up mods.");
            return;
        }

        if(userMods == null)
        {
            Debug.LogError("Recieved user mods are null.");
        }
        this.upgradeMods = userMods;
        userMods.AddObserver(this);
        init = true;
    }

    internal void Unregister()
    {
        upgradeMods.RemoveObserver(this);
    }

    void OnDestroy()
    {
        if (init)
        {
            if (upgradeMods != null)
                upgradeMods.RemoveObserver(this);
            else Debug.Log("Upgrade logs is already null, can't remove observer.");
        }
    }

    public float GetSum()
    {
        if(upgradeMods != null)
            logSum = upgradeMods.GetModSum();
        logSum = 0;
        return logSum;
    }

    public override void Notified(float value)
    {
        Debug.Log("new value "+ value);
    }
}
