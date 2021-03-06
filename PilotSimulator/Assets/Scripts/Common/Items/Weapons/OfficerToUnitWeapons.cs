﻿using System.Collections.Generic;
using UnityEngine;

public class OfficerToUnitWeapons:MonoBehaviour, IWeaponChanger, IRandomizable
{

    public TacticGroup group;

    public int startWeaponInGroup = -1;
    public int activeWeaponInGroup = -1;

    private void Start()
    {
        Change(startWeaponInGroup);
    }

    public void EvtOnSpawnSetUnitWeapon()
    {
        Change(activeWeaponInGroup);
    }

    public void Change(int id)
    {
        GroupChangeWeapon(id);
    }

    public void GroupChangeWeapon(int swap)
    {
        activeWeaponInGroup = swap;
        List<TacticallyConnected> units = group.Units;
        for (int i = 0; i < units.Count; i++)
        {
            units[i].ChangeWeapon(swap);
        }
        //Debug.Log("Changed weapon for unit count of "+ group.Units.Count+" to id "+swap);
    }

    public void RandomValue(int next)
    {
        GroupChangeWeapon(next);
    }
}
