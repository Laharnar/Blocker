using UnityEngine;

public class UIId : MonoBehaviour
{
    public int id;
    public MonoConnection weaponPicker;

    public void PickWeapon()
    {
        weaponPicker.MonoWeaponChanger.Change(id);
    }
}
