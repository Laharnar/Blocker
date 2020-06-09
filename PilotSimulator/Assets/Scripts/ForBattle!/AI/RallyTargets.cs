using System.Collections.Generic;
using UnityEngine;

public static class RallyTargets
{
    public static Dictionary<int, List<Target>> targets = new Dictionary<int, List<Target>>();

    public static void AddRally(Vector3 position, int alliance)
    {
        if (!targets.ContainsKey(alliance))
            targets.Add(alliance, new List<Target>());
 
        targets[alliance].Add(TargetFactory.GetRallyPoint(position, alliance));
    }
    public static void SetRally(Vector3 position, int alliance)
    {
        if (!targets.ContainsKey(alliance))
            targets.Add(alliance, new List<Target>());
        targets[alliance].Clear();
        targets[alliance].Add(TargetFactory.GetRallyPoint(position, alliance));
    }

    public static Target Closest(Vector3 position)
    {
        if (targets.Count > 0)
        {
            return GetClosestTarget(position, targets[0]);
        }
        return null;
    }

    private static Target GetClosestTarget(Vector3 position, List<Target> search)
    {
        Target target = null;
        float minDist = float.MaxValue;
        for (int i = 0; i < search.Count; i++)
        {
            float dist = Vector2.Distance(search[i].target, position);
            if (dist < minDist)
            {
                minDist = dist;
                target = search[i];
            }
        }
        return target;
    }
}
