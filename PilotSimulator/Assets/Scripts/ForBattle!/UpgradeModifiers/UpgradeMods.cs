using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeMods:IUserMods {

    [SerializeField] List<UpgradeData> mods;
    [SerializeField] string modType;
    public string ModType { get => modType; }
    public Action<float> OnModGetsNewValue { get; set; }

    public void Add(UpgradeData mod)
    {
        Debug.Log("Added mod");
        mods.Add(mod);
        OnModGetsNewValue?.Invoke(mod.increase);
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

    public UpgradeMods HardCopy()
    {
        UpgradeMods copied = new UpgradeMods();
        copied.mods = new List<UpgradeData>();
        for (int i = 0; i < mods.Count; i++)
        {
            copied.mods.Add(mods[i].Copy());
        }
        copied.modType = modType;
        return copied;
    }
}
