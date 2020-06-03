using UnityEngine;

public class WeaponPickerUI : MonoBehaviour
{
    public MonoConnection weaponChanger;

    public void PickWeaponByUI(int id)
    {
        if(weaponChanger.MonoWeaponChanger != null) 
            weaponChanger.MonoWeaponChanger.Change(id);
    }
}
