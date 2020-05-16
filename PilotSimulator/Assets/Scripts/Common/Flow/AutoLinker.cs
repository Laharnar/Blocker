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
            LinkerRoot target = t.GetComponent<LinkerRoot>();
            RealtimeTester.Assert(target != null, t, "Spawned object doesn't have LinkerRoot script. " + t.name);

            target.Setup(linkedSelf);
            if(expgroup) expgroup.ConnectToChild(t);

            if (upgrades)
            {
                ConnectUpgradesToChild(t);
            }
        }
    }

    private void ConnectUpgradesToChild(Transform t)
    {
        IInitializer user = t.GetComponentInChildren<IInitializer>();
        user.InitOnSpawn("upgrades", upgrades);
    }
}
