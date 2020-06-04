using System.Collections.Generic;
using UnityEngine;

public class BonusesToMods:MonoBehaviour
{
    [SerializeField] List<StatMods> mods = new List<StatMods>();

    public void Assign(BonusList bonuses)
    {
        for (int i = 0; i < bonuses.Count; i++)
        {
            GetModReference(bonuses.GetBonus(i));
        }
    }

    public void Unassign(BonusList bonuses)
    {
        for (int i = 0; i < bonuses.Count; i++)
        {
            GetModReference(bonuses.GetBonus(i));
        }
    }

    public StatMods GetModReference(string modType)
    {
        for (int i = 0; i < mods.Count; i++)
        {
            Debug.Log("TODO uncomment.");
            //if (mods[i].StatType == modType)
            {
                return mods[i];
            }
        }
        return null;
    }

    public StatMods GetModReference(Bonus bonus)
    {
        return GetModReference(bonus.type);
    }
}