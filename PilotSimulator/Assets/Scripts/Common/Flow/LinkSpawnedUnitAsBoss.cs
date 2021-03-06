﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinkSpawnedUnitAsBoss
{
    [SerializeField] bool isBossSpawner = false;
    [SerializeField] List<TacticalBases> otherGenerals;

    public void ToEnemyBoss(Transform spawned)
    {
        CombatUser spawnedBoss = spawned.GetComponent<CombatUser>();
        if (spawnedBoss)
        {
            if (isBossSpawner)
            {
                for (int i = 0; i < otherGenerals.Count; i++)
                {
                    otherGenerals[i].enemyBoss = spawnedBoss;
                }
            }
        }
    }
}
