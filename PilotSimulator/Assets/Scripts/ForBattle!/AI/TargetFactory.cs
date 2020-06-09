using UnityEngine;

public static class TargetFactory
{
    public static Target GetRallyPoint(Vector3 point, int alliance)
    {
        return new Target(
            point,
            0
        );
    }
}
