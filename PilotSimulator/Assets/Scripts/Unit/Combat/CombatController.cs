using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

// GOAL: split into 4 -> 
// Repurposed into tactic data container and controller for sub systems.
public class CombatController : StatMods
{
    public bool used = true;
    [SerializeField] CombatUser self;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] bool attackingLocked = false;

    [Header("Attacking")]
    [SerializeField] CombatCollisionTrigger attackRange;
    [SerializeField] AttackAction basicAttackDamage = new AttackAction(1);
    [SerializeField] float attackLength = 0.1f;
    [SerializeField] float waitBetweenAttacks = 2f;

    [Header("Traversal")]
    [SerializeField] Traversal traversal;
    [SerializeField] BlockableMovement blocking;

    [Header("Searching")]
    [SerializeField] int searchByEnemyFlagId = 0;

    [Header("Logging")]
    CombatUser[] logAll;
    [SerializeField] List<CombatUser> logEnemies;

    float Damage { get => basicAttackDamage.GetDamage(); }
    public bool IsBlocked { get => blocking.isBlocked; }


    // Update is called once per frame
    void Update()
    {
        if (attackingLocked) return;
        if (!used) return;
        AttackClosest();
    }

    public bool IsInAttackRange(CombatUser enemy)
    {
        return enemy && attackRange.IsInRange(enemy.transform);
    }
    

    internal bool LockdownEnemy(CombatUser enemy)
    {
        if (!enemy)
        {
            return false;
        }
        // blocking is optional.
        BlockableMovement other = enemy.con8.blocking;
        other?.LockMovement(this);
        return true;
    }

    internal bool ReleaseEnemy(CombatUser enemy)
    {
        if (!enemy)
        {
            return false;
        }
        // blocking is optional.
        BlockableMovement other = enemy.con8.blocking;
        other?.UnlockMovement(this);
        return true;
    }

    public void Travel(Vector3 goTo)
    {
        traversal.Travel(goTo);
    }
    public void Stop()
    {
        StopWalking();
    }

    public void StopWalking()
    {
        traversal.Travel(transform.position);
    }

    public void AttackClosest()
    {
        // very lazy and slow search for enemies.
        CombatUser enemyToFollow = FindClosestAttackableEnemy();
        NormalAttack(enemyToFollow);
    }
    /// <summary>
    /// Distance based attacking.
    /// </summary>
    /// <param name="enemy"></param>
    public void NormalAttack(CombatUser enemy)
    {
        // when unit is in defence area, look for enemies in that area
        Follow(enemy);
        
        if (IsInAttackRange(enemy))
        {
            AttackEnemy(enemy);
            //-
            Stop();// fix for not having moddable move range same as attack range.
            //-
        }
    }

    public void OffensiveLocking(CombatUser enemyToFollow)
    {
        // when unit is in defence area, look for enemies in that area
        Follow(enemyToFollow);
        LockdownHit(enemyToFollow);
    }

    public void LockdownHit(CombatUser enemyToFollow)
    {
        if (IsInAttackRange(enemyToFollow))
        {
            LockdownEnemy(enemyToFollow);
            AttackEnemy(enemyToFollow);
        }
    }

    internal CombatUser SearchEnemy()
    {
        return FindClosestAttackableEnemy();
    }

    internal void Follow(CombatUser enemyToFollow)
    {
        if (enemyToFollow)
        {
            // walk to enemy
            Travel(enemyToFollow.transform.position);
        }
    }

    internal void FollowAgressively(CombatUser enemyToFollow)
    {
        if (enemyToFollow)
        {
            // walk to enemy
            Travel(enemyToFollow.transform.position);
        }
    }

    internal void AttackEnemy(CombatUser enemyToFollow)
    {
        if (attackingLocked) return;
        StartCoroutine(AttackRoutine(enemyToFollow));
    }

    // ----- private functions -----

    private IEnumerator AttackRoutine(CombatUser attacked)
    {
        if (attackingLocked) yield break;
        attackingLocked = true;

        Debug.Log(gameObject.transform.root.name + " Attacking " + attacked +" for "+ Damage);
        attacked.Damage((int)Damage);

        sprite.color = Color.red;
        yield return new WaitForSeconds(attackLength);
        sprite.color = Color.white;

        yield return new WaitForSeconds(waitBetweenAttacks);
        attackingLocked = false;
    }

    private CombatUser FindClosestAttackableEnemy()
    {
        return
            FindClosestInGroup(
                FindAttackableEnemies()
                );
    }

    private CombatUser FindClosestInGroup(List<CombatUser> group)
    {
        if (group.Count == 0) return null;

        int closestEnemyId = ClosestInGroupId(group);
        if (closestEnemyId == -1)
        {
            Debug.LogError("Enemy not picked.");
            return null;
        }
        return group[closestEnemyId];
    }

    private List<CombatUser> FindAttackableEnemies()
    {
        List<CombatUser> attackableEnemies, enemies;
        enemies = SearchSceneForEnemies();
        attackableEnemies = GetAttackable(enemies);
        logEnemies = attackableEnemies;
        return attackableEnemies;
    }

    private List<CombatUser> SearchSceneForEnemies()
    {
        List<CombatUser> enemies;
        CombatUser[] all = GameObject.FindObjectsOfType<CombatUser>();
        enemies = GetUnitsByFlag(all.ToList(), searchByEnemyFlagId);
        logAll = all;
        return enemies;
    }

    private static List<CombatUser> GetUnitsByFlag(List<CombatUser> mixed, int allyId)
    {
        List<CombatUser> allies = new List<CombatUser>();
        for (int i = 0; i < mixed.Count; i++)
        {
            if (mixed[i].alliance.thisAlliance == allyId)
            {
                allies.Add(mixed[i]);
            }
        }
        return allies;
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


    private int ClosestInGroupId(List<CombatUser> users)
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


    [System.Serializable]
    public class Traversal
    {
        [SerializeField] List<MovementPlanning> traversal;

        public void Travel(Vector2 goTo)
        {
            for (int i = 0; i < traversal.Count; i++)
            {
                traversal[i].OverwriteTargetAsFirst(goTo);
            }
        }
    }

}
