using UnityEngine;

public class WeaponMaker:MonoBehaviour, IWeaponMaker
{
    public Transform weaponsListPrefab;

    public Transform CreateWeapon(int id, UnitWeapons target)
    {
        Transform t = weaponsListPrefab.GetChild(id);
        Transform newWeapon = Instantiate(t, target.transform);
        return newWeapon;
    }

    // Optionally use RemoveWeapon directly.
    public void DestroyWeapon(UnitWeapons target)
    {
        target.RemoveWeapon();
    }
}