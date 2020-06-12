using System.Collections.Generic;
using UnityEngine;

public class TacticallyConnected:MonoBehaviour, ISetupUnity
{
    [SerializeField] internal TacticalUnit asUser;
    [SerializeField] internal UnitWeapons asWeaponUser;
    [Header("| Linked |")]
    [SerializeField] internal TacticGroup officer;
    [SerializeField] internal UnitScriptLoader scripts;

    private void OnDisable()
    {
        if (officer) officer.DisconnectUnitOnDestroy(this);
    }

    internal void Activate(int id)
    {
        if(asUser)asUser.Activate(id);
    }

    internal void SetEnemyBoss(CombatUser enemyBoss)
    {
        if (asUser) asUser.EnemyBossToAttack = enemyBoss;
    }

    internal void SetAllyBase(Transform defendingBase)
    {
        if (asUser) asUser.AllyBaseToDefend = defendingBase;
    }

    internal void ChangeWeapon(int swap)
    {
        if (asWeaponUser)
        {
            asWeaponUser.Change(swap);
        }
        else
        {
            Debug.Log("Attemting to change weapon on use without Weapons."+name, this);
        }
    }

    public bool UnitySetup()
    {
        if (!scripts)
        {
            scripts = GetComponent<UnitScriptLoader>();
        }
        if (scripts)
            return true;
        return false;
    }
}
