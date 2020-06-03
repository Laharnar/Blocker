using UnityEngine;

public interface IWeaponMaker
{
    Transform CreateWeapon(int id, UnitWeapons target);
}
