using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMods: Observer
{
    [SerializeField] float logSum = 0;
    [SerializeField] protected UpgradeMods upgradeMods;
    [SerializeField] bool init = false;

    public virtual void SetMods(UpgradeMods userMods)
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
        logSum = 0;
        if (upgradeMods != null)
            logSum = upgradeMods.GetModSum();
        return logSum;
    }

    public override void Notified(float value)
    {
        Debug.Log("new value "+ value);
    }
}
