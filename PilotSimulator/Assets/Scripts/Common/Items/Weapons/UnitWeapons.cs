using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitWeapons : MonoBehaviour //, ITestable
{
    [SerializeField] int startupEquipped = -1;

    [SerializeField] int equipped = -1;
    [SerializeField] GameObject equippedObj;
    [SerializeField] Transform hand;
    [SerializeField] MonoConnection maker;

    private void Start()
    {
        if(startupEquipped > -1)
            ChangeWeapon(startupEquipped);
    }

    public void ChangeWeapon(int id)
    {
        RemoveWeapon();

        equipped = id;
        Transform newWeapon = maker.MonoWeaponMaker.CreateWeapon(id, this);
        UseAsWeapon(newWeapon);
    }

    public void RemoveWeapon()
    {
        equipped = -1;
        if (equippedObj)
        {
            if(Application.isPlaying)
                Destroy(equippedObj);
            else
                DestroyImmediate(equippedObj);
        }
    }

    public void UseAsWeapon(Transform obj)
    {
        if (obj)
        {
            equippedObj = obj.gameObject;
            obj.transform.parent = hand;
        }
    }

    // Testing changes.
    /*public void TestInitialState()
    {
        if(equipped == 0)
            ChangeWeapon(1);
        else
            ChangeWeapon(0);
    }*/

}
