using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeMod:IUpgradeMods {

    [SerializeField] List<IModData> mods = new List<IModData>();
    [SerializeField] List<UpgradeData> upmods = new List<UpgradeData>();
    [SerializeField] string modType;
    public string ModType { get => modType; }
    public int ModCount { get => mods.Count; }

    [SerializeField] Listener evtOnAddedMod = new Listener();


    public void AddMod(IModData newMod)
    {
        mods.Add(newMod);
        if(newMod as UpgradeData != null)
            upmods.Add((UpgradeData)newMod);
        Debug.Log("adding mod "+newMod.ModType+ newMod.ModValue);
        evtOnAddedMod.Notify(newMod);
    }

    public float GetModSum()
    {
        float sum = 0;
        for (int i = 0; i < mods.Count; i++)
        {
            sum += mods[i].ModValue;
        }
        return sum;
    }

    public UpgradeMod HardCopy()
    {
        UpgradeMod copied = new UpgradeMod();
        copied.mods = new List<IModData>();
        for (int i = 0; i < mods.Count; i++)
        {
            copied.mods.Add(mods[i].Copy());
        }
        copied.modType = modType;
        copied.ResetObservers();
        return copied;
    }

    public UpgradeMod GetDataCopyForResettingModSets()
    {
        return HardCopy();
    }

    private void ResetObservers()
    {
        evtOnAddedMod = new Listener();
    }
    public void AddObserver(StatMods mod)
    {
        evtOnAddedMod.RegisterObserver(mod);
    }

    public void RemoveObserver(StatMods mod)
    {
        if (evtOnAddedMod != null)
            evtOnAddedMod.UnregisterObserver(mod);
        else
            Debug.Log("Observer is already null.");
    }

    internal void AddMods(UpgradeMod upgradeMods)
    {
        for (int i = 0; i < upgradeMods.mods.Count; i++)
        {
            mods.Add(upgradeMods.mods[i]);
        }
    }
}
