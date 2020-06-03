using UnityEngine;

public class WeaponPickerUI : MonoBehaviour
{
    public UnitWeapons activeUser;

    public void PickWeaponByUI(int id)
    {
        if(activeUser) activeUser.ChangeWeapon(id);
    }
}
