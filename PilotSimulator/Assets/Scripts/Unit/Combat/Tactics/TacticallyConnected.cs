﻿using System;
using UnityEngine;

public class TacticallyConnected:MonoBehaviour
{
    [SerializeField] internal TacticalUnit asUser;
    [Header("| Linked |")]
    [SerializeField] internal TacticGroup officer;

    private void OnDestroy()
    {
        if (officer) officer.DisconnectUnitOnDestroy(this);
    }

    internal void Activate(int id)
    {
        if(asUser)asUser.Activate(id);
    }

    internal void SetEnemyBoss(CombatUser enemyBoss)
    {
        if (asUser) asUser.EnemyBossToAttack = enemyBoss;
    }

    internal void SetAllyBase(Transform defendingBase)
    {
        if (asUser) asUser.AllyBaseToDefend = defendingBase;
    }
}
