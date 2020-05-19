using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeMods:IUserMods {
    [SerializeField]List<UpgradeData> mods = new List<UpgradeData>();

    public string ModType { get; }
    public Action<float> OnModGetsNewValue { get; set; }

    public void Add(UpgradeData mod)
    {
        mods.Add(mod);
        if(OnModGetsNewValue != null) OnModGetsNewValue(mod.increase);
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
}
