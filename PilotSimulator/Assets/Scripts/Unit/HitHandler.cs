using UnityEngine;
// todo: remove
public static class HitHandler {

    public static void HandleHit(OnHit from, DamageReciever to)
    {
        if(GlobalStorage.TransformKeysExist(from, to))
        {
            DealDamageToEnemies(from, to);
        }
        else
        {
            ReportCannotHandleError(from.name+" "+to.name);
        }
    }

    private static void ReportCannotHandleError(string suffix)
    {
        throw new System.NullReferenceException("Global storage didn't return transform for either of " + suffix);
    }

    private static void DealDamageToEnemies(OnHit from, DamageReciever to)
    {
        DealDamageToEnemies(from.transform, to.transform);
    }

    private static void DealDamageToEnemies(Transform from, Transform to)
    {
        if (!GlobalStorage.AlliancesMatch(from, to))
        {
            GlobalStorage.GlobalOnHitHandler(from, to);
        }
    }
}
