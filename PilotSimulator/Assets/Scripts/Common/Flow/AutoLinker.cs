using System;
using UnityEngine;

[System.Serializable]
public class AutoLinker
{

    public bool autoLink = false;
    public Linked linkedSelf;
    public ExpGroup expgroup;
    public SimpleUpgrades upgrades;

    public void SetupLink(Transform t)
    {
        if (autoLink)
        {
            // connect script data from spawner to spawned
            LinkerRoot target = t.GetComponent<LinkerRoot>();
            RealtimeTester.Assert(target != null, t, "Spawned object doesn't have LinkerRoot script. " + t.name);

            if(linkedSelf)target.Setup(linkedSelf);
            if(expgroup) expgroup.ConnectToChild(t);

            if (upgrades)
            {
                ConnectUpgradesToChild(t);
            }
        }
    }

    private void ConnectUpgradesToChild(Transform t)
    {
        ISpawnInitializer user = t.GetComponentInChildren<ISpawnInitializer>();
        user.InitOnSpawn("upgrades", upgrades);
    }

    internal void TestInitialState(MonoBehaviour t)
    {
        RealtimeTester.AssertSceneReference(linkedSelf, t);
    }
}
