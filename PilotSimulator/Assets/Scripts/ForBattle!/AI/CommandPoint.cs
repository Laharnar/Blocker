using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandPoint:MonoBehaviour
{
    public Transform target;

    public void AddAsRallyAlly()
    {
        RallyTargets.SetRally(target.position, 0);
    }

    public void AddAsRallyEnemy()
    {
        RallyTargets.SetRally(target.position, 1);
    }
}