using System.Collections.Generic;
using UnityEngine;

public class BonusesToMods:MonoBehaviour
{
    [System.Serializable]
    public class UniqueStatModsList {
        // References for relaying stat mods to other scripts.
        [SerializeField] List<StatMods> mods = new List<StatMods>();
        public bool nonUniqueThrowError = true;
        [SerializeField] List<StatMods> logIsDuplicate = new List<StatMods>();
        bool changed = true;

        void CheckDuplicates()
        {
            if (!changed || nonUniqueThrowError == false) return; 

            HashSet<StatMods> unique = new HashSet<StatMods>();
            for (int i = 0; i < mods.Count; i++)
            {
                if (!unique.Add(mods[i]))
                {
                    Debug.LogError("Mod is duplicate, which is not allowed.", mods[i]);
                }
            }
        }

        public StatMods GetModReference(string modType)
        {
            CheckDuplicates();
            for (int i = 0; i < mods.Count; i++)
            {
                if (mods[i].StatType == modType)
                {
                    return mods[i];
                }
            }
            return null;
        }
    }

    [SerializeField]UniqueStatModsList uniqueStats;

    [ContextMenu("Assign test")]
    void TestAssign()
    {
        BonusList bl = GetComponentInChildren<BonusList>();
     
        if(bl) Assign(bl);
    }

    public void Assign(BonusList bonuses)
    {
        for (int i = 0; i < bonuses.Count; i++)
        {
            Bonus bonus = bonuses.GetBonus(i);
            StatMods mods = GetModReference(bonus);
            mods.SwapWeaponBonus(bonus);
        }
    }

    public void Unassign(BonusList bonuses)
    {
        for (int i = 0; i < bonuses.Count; i++)
        {
            GetModReference(bonuses.GetBonus(i));
        }
    }


    public StatMods GetModReference(Bonus bonus)
    {
        return uniqueStats.GetModReference(bonus.type);
    }
}