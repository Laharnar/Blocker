using UnityEngine;

public class WeaponPickerUI : MonoBehaviour, IWeaponChanger
{
    public MonoConnection weaponChanger;

    public void Change(int id)
    {
        PickWeaponByUI(id);
    }

    public void PickWeaponByUI(int id)
    {
        if(weaponChanger.MonoWeaponChanger != null) 
            weaponChanger.MonoWeaponChanger.Change(id);
    }
}
