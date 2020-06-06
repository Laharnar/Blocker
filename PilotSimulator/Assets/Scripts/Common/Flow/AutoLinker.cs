using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
[System.Serializable]
public class AutoLinker
{
    // Calls initializers for selected(probably spawned) target.

    public bool autoLink = false;
    public Linked linkedSelf;
    public ExpGroup expgroup;
    public SimpleUpgrades upgrades;
    [Header("Optional")]
    public UpgradableUser userUpgrades; 
    public UpgradableAlliance allies;
    [SerializeField] TacticLinker tactics;
    [SerializeField] LinkSpawnedUnitAsBoss boss;
    [SerializeField] OfficerToUnitWeapons weapons;

    public void SetupLink(Transform t)
    {
        if (autoLink)
        {
            // connect script data from spawner to spawned
            LinkerRoot target = t.GetComponent<LinkerRoot>();
            RealtimeTester.Assert(target != null, t, "Spawned object doesn't have LinkerRoot script. " + t.name);

            if(linkedSelf) target.Setup(linkedSelf);
            if(expgroup) expgroup.ConnectExpToChild(t);
            if (upgrades) ConnectUpgradesToChild(t);

            // set up connection to source
            tactics.ConnectTactics(t);

            // everything that relies on connection back to source.
            boss.ToEnemyBoss(t);
            
            weapons?.EvtOnSpawnSetUnitWeapon();
        }
    }

    private void ConnectUpgradesToChild(Transform t)
    {
        ISpawnUpgradeInitializer user = t.GetComponentInChildren<ISpawnUpgradeInitializer>();
        user.InitUpgradesOnSpawn(upgrades);

        
        //UpgradableUser upgradableUser = t.GetComponentInChildren<UpgradableUser>();
        //if (upgradableUser)
        //    UpgradableUser.Connect(upgradableUser, expgroup);
        //else Debug.LogErrorFormat(t, "User isn't on t:{0}", upgradableUser);
        if (allies && userUpgrades)
            allies.Add(userUpgrades);
        else Debug.Log("allies && userUpgrades aren't connected optionally in auto linker.", t);
    }

    internal void TestInitialState(MonoBehaviour t)
    {
        RealtimeTester.AssertSceneReference(linkedSelf, t);
    }
}
