using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinkSpawnedUnitAsBoss
{
    [SerializeField] bool isBossSpawner = false;
    [SerializeField] List<TacticsCommand> otherGenerals;

    public void ToEnemyBoss(CombatUser spawned)
    {
        if (isBossSpawner)
        {
            for (int i = 0; i < otherGenerals.Count; i++)
            {
                otherGenerals[i].enemyBoss = spawned;
            }
        }
    }
}
