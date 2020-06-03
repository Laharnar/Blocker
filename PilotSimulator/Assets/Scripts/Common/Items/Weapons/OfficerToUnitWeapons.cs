using System.Collections.Generic;
using UnityEngine;

public class OfficerToUnitWeapons:MonoBehaviour, IWeaponChanger
{

    public TacticGroup group;

    public int startWeaponInGroup = -1;
    public int activeWeaponInGroup = -1;

    private void Start()
    {
        Change(startWeaponInGroup);
    }

    public void Change(int id)
    {
        GroupChangeWeapon(id);
    }

    [ContextMenu("Change weapon")]
    void TestChangeWeapon1()
    {
        GroupChangeWeapon(0);
    }
    void TestChangeWeapon2()
    {
        GroupChangeWeapon(1);
    }
    public void GroupChangeWeapon(int swap)
    {
        activeWeaponInGroup = swap;
        List<TacticallyConnected> units = group.Units;
        for (int i = 0; i < units.Count; i++)
        {
            units[i].ChangeWeapon(swap);
        }
    }

}
