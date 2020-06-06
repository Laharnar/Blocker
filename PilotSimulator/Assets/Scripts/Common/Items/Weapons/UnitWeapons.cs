using System.Collections;
using UnityEngine;

public class UnitWeapons : MonoBehaviour, IWeaponChanger, ISetupUnity
{
    [SerializeField] int startupEquipped = -1;

    [SerializeField] int equipped = -1;
    [SerializeField] GameObject equippedObj;
    [SerializeField] Transform hand;
    [SerializeField] MonoConnection maker;
    [SerializeField] BonusesToMods weaponBonuses;
    [SerializeField] bool init = true;

    private void Start()
    {
        if (!init)
        {
            Debug.Log("startup equip");
            ChangeWeapon(startupEquipped);
            init = true;
        }
    }

    public void ChangeWeapon(int id)
    {
        Debug.Log("Changed weapon from "+equipped +" to " +id);
        RemoveWeapon();

        equipped = id;
        if (id > -1)
        {
            Transform newWeapon = maker.MonoWeaponMaker.CreateWeapon(id, this);
            UseAsWeapon(newWeapon);
        }
    }

    public void RemoveWeapon()
    {
        equipped = -1;
        if (equippedObj)
        {
            if (Application.isPlaying)
            {
                Destroy(equippedObj);
            }
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
            obj.transform.localPosition = new Vector3(0,0,0);

            if (weaponBonuses)
            {
                BonusList mods = obj.GetComponent<BonusList>();
                if (mods)
                {
                    weaponBonuses.Assign(mods);
                }
            }
        }
    }

    public void Change(int id)
    {
        ChangeWeapon(id);
        init = true;
    }


    public bool UnitySetup()
    {
        RealtimeTester.Assert(weaponBonuses != null, this, "Weapon bonus is null.");
        return true;
    }
}
