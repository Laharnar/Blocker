﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CombatController : MonoUserMods, ITestable
{
    public CombatUser self;

    public SpriteRenderer sprite;
    public bool attackingLocked = false;

    public CombatCollisionTrigger attackRange;
    public AttackAction basicAttackDamage = new AttackAction(1);

    public float attackLength = 0.1f;
    public float waitBetweenAttacks = 2f;
    public int searchForEnemyId = 0;
    public int searchByEnemyFlagId = 0;

    [SerializeField] MovementPlanning goTo;
    [SerializeField] private CombatUser[] logAll;
    [SerializeField] private List<CombatUser> logEnemies;
    [SerializeField] int logPickedEnemyTargetId= 0;
    [SerializeField] private CombatUser logEnemyTargat;
   

    // Update is called once per frame
    void Update()
    {
        if (attackingLocked) return;
        Attack();
    }

    private void Attack()
    {
        // very lazy and slow search for enemies.
        CombatUser[] all = GameObject.FindObjectsOfType<CombatUser>();
        List<CombatUser> enemies = GetUnitsByFlag(all.ToList(), searchByEnemyFlagId);
        List<CombatUser> attackableEnemy = GetAttackable(enemies);
        CombatUser enemyToFollow = null;
        if (attackableEnemy.Count > 0)
        {
            int closestEnemyId = Closest(attackableEnemy);
            if(closestEnemyId == -1)
            {
                Debug.LogError("Enemy not picked.");
            }
            else
            {
                enemyToFollow = enemies[closestEnemyId];
                
                // walk to enemy
                goTo.OverwriteTargetAsFirst(enemyToFollow.transform.position);
            }
        }
        logAll = all;
        logEnemies = attackableEnemy;
        logEnemyTargat = enemyToFollow;

        
        if (enemyToFollow)
        {
            if (Vector3.Distance(enemyToFollow.transform.position, transform.position) < attackRange.Range)
            {
                StartCoroutine(AttackRoutine(enemyToFollow));
            }
        }
        else
        {
            Debug.LogError("Enemy count is 0 when searching for " + searchForEnemyId, this);
        }
    }

    private List<CombatUser> GetAttackable(List<CombatUser> group)
    {
        List<CombatUser> attackable = new List<CombatUser>();
        for (int i = 0; i < group.Count; i++)
        {
            if (group[i].IsAttackable)
            {
                attackable.Add(group[i]);
            }
        }
        return group;
    }

    float Damage{ get => basicAttackDamage.GetDamage(userMods); }

    private static List<CombatUser> GetUnitsByFlag(List<CombatUser> mixed, int allyId)
    {
        List<CombatUser> allies = new List<CombatUser>();
        for (int i = 0; i < mixed.Count; i++)
        {
            if(mixed[i].alliance.thisAlliance == allyId)
            {
                allies.Add(mixed[i]);
            }
        }
        return allies;
    }

    private int Closest(List<CombatUser> users)
    {
        int closest = -1;
        float minDist = float.MaxValue;
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i] == self || users[i] == null) continue;

            float dist = Vector3.Distance(users[i].transform.position, self.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = i;
            }
        }
        return closest;
    }

    private IEnumerator AttackRoutine(CombatUser attacked)
    {
        attackingLocked = true;
        
        Debug.Log(gameObject.transform.root.name + " Attacking "+attacked);
        attacked.Damage((int)Damage);

        sprite.color = Color.red;
        yield return new WaitForSeconds(attackLength);
        sprite.color = Color.white;

        yield return new WaitForSeconds(waitBetweenAttacks);
        attackingLocked = false;
    }

    public void TestInitialState()
    {
        searchByEnemyFlagId = searchForEnemyId;
    }
}
