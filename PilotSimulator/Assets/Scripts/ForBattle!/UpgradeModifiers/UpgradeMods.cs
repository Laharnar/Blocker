using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeMods:IUpgradeMods {

    [SerializeField] List<UpgradeData> mods;
    [SerializeField] string modType;
    public string ModType { get => modType; }

    [SerializeField] Listener addNewModsEvt = new Listener();

    public void AddMod(UpgradeData newMod)
    {
        Debug.Log("Added mod");
        mods.Add(newMod);
        addNewModsEvt.Notify(newMod.increase);
    }

    public float GetModSum()
    {
        float sum = 0;
        for (int i = 0; i < mods.Count; i++)
        {
            sum += mods[i].increase;
        }
        return sum;
    }


    public void AddObserver(StatMods mod)
    {
        addNewModsEvt.RegisterObserver(mod);
    }

    public void RemoveObserver(StatMods mod)
    {
        if (addNewModsEvt != null)
            addNewModsEvt.UnregisterObserver(mod);
        else
            Debug.Log("Observer is already null.");
    }

    public UpgradeMods HardCopy()
    {
        UpgradeMods copied = new UpgradeMods();
        copied.mods = new List<UpgradeData>();
        for (int i = 0; i < mods.Count; i++)
        {
            copied.mods.Add(mods[i].Copy());
        }
        copied.modType = modType;
        copied.ResetObservers();
        return copied;
    }

    private void ResetObservers()
    {
        addNewModsEvt = new Listener();
    }
}
