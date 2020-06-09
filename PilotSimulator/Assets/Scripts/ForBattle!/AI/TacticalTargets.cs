using System.Collections;
using UnityEngine;
public class TacticalTargets:MonoBehaviour
{
    public Transform[] manualTargets;

    public void AddAsRallyAllyManual(int id)
    {
        RallyTargets.SetRally(manualTargets[id].position, 0);
    }
    public void AddAsRallyEnemyManual(int id)
    {
        RallyTargets.SetRally(manualTargets[id].position, 1);
    }

    public void AddAsRallyAlly(Transform target)
    {
        RallyTargets.SetRally(target.position, 0);
    }
    public void AddAsRallyEnemy(Transform target)
    {
        RallyTargets.SetRally(target.position, 1);
    }
}
