using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

// GOAL: split into 4 -> combat controller. movement follow target. targeting searching. 
// Combat controller has control over which items are ran.
// Repurposed into tactic.
public class CombatController : StatMods, ITestable
{
    public bool used = true;
    [SerializeField] CombatUser self;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] bool attackingLocked = false;

    [SerializeField] BlockableMovement blocking;
    [SerializeField] CombatCollisionTrigger attackRange;
    [SerializeField] AttackAction basicAttackDamage = new AttackAction(1);
    [SerializeField] float attackLength = 0.1f;
    [SerializeField] float waitBetweenAttacks = 2f;

    [Header("Traversal")]
    [SerializeField] Traversal traversal;

    [Header("Searching")]
    [SerializeField] int searchByEnemyFlagId = 0;

    [Header("Logging")]
    [SerializeField] CombatUser[] logAll;
    [SerializeField] List<CombatUser> logEnemies;
    [SerializeField] int logPickedEnemyTargetId= 0;

    float Damage { get => basicAttackDamage.GetDamage(); }
    public bool IsBlocked { get => blocking.isBlocked; }

    public float AttackRange { get => attackRange.Range; }

    // Update is called once per frame
    void Update()
    {
        if (attackingLocked) return;
        if (!used) return;
        Attack();
    }

    internal bool LockdownEnemy(CombatUser enemy)
    {
        if (!enemy)
        {
            return false;
        }
        if (!enemy.con8) { 
            Debug.LogError("Enemy isn't connected con8.", enemy);
            return false; 
        }
        // blocking is optional.
        if (!enemy.con8.blocking) { 
            return true; 
        }

        BlockableMovement other = enemy.con8.blocking;
        other.LockMovement(this);
        return true;
    }

    internal bool ReleaseEnemy(CombatUser enemy)
    {
        if (!enemy)
        {
            return false;
        }
        if (!enemy.con8)
        {
            Debug.LogError("Enemy isn't connected con8.", enemy);
            return false;
        }
        // blocking is optional.
        if (!enemy.con8.blocking)
        {
            return true;
        }

        BlockableMovement other = enemy.con8.blocking;
        other.UnlockMovement(this);
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

    private void Attack()
    {
        // very lazy and slow search for enemies.
        CombatUser enemyToFollow = FindClosestAttackableEnemy();
        Follow(enemyToFollow);

        if (enemyToFollow)
        {
            if (Vector3.Distance(enemyToFollow.transform.position, transform.position) < attackRange.Range)
            {
                StartCoroutine(AttackRoutine(enemyToFollow));
            }
        }
        else
        {
            Debug.Log("!Enemy count is 0 when searching for " + searchByEnemyFlagId, this);
        }
    }
    /// <summary>
    /// Distance based attacking.
    /// </summary>
    /// <param name="enemy"></param>
    public void NormalAttack(CombatUser enemy)
    {
        // when unit is in defence area, look for enemies in that area
        Follow(enemy);

        if (enemy)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < AttackRange)
            {
                AttackEnemy(enemy);
            }
        }
    }

    public void OffensiveLocking(CombatUser enemyToFollow)
    {
        // when unit is in defence area, look for enemies in that area
        Follow(enemyToFollow);

        if (enemyToFollow)
        {
            if (Vector3.Distance(enemyToFollow.transform.position, transform.position) < AttackRange)
            {
                LockdownEnemy(enemyToFollow);
                AttackEnemy(enemyToFollow);
            }
        }
    }

    private CombatUser FindClosestAttackableEnemy()
    {
        return 
            FindClosest(
                FindAttackableEnemies()
                );
    }

    private List<CombatUser> FindAttackableEnemies()
    {
        List<CombatUser> attackableEnemies, enemies;
        enemies = SearchSceneForEnemies();
        attackableEnemies = GetAttackable(enemies);
        logEnemies = attackableEnemies;
        return attackableEnemies;
    }

    private CombatUser FindClosest(List<CombatUser> group) { 
        CombatUser unit = null;
        if (group.Count > 0)
        {
            int closestEnemyId = Closest(group);
            if (closestEnemyId == -1)
            {
                Debug.LogError("Enemy not picked.");
            }
            else
            {
                unit = group[closestEnemyId];

            }
        }
        return unit;
    }

    private List<CombatUser> SearchSceneForEnemies()
    {
        List<CombatUser> enemies;
        CombatUser[] all = GameObject.FindObjectsOfType<CombatUser>();
        enemies = GetUnitsByFlag(all.ToList(), searchByEnemyFlagId);
        logAll = all;
        return enemies;
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
    }


    public void Activate()
    {
        used = true;
    }

    public void Deactivate()
    {
        used = false;
        OnLoseFocusTactic.Do(this);
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

    internal void AttackEnemy(CombatUser enemyToFollow)
    {
        if (attackingLocked) return;
        StartCoroutine(AttackRoutine(enemyToFollow));
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
public static class OnLoseFocusTactic
{
    public static void Do(CombatController controller)
    {
        controller.used = false;
        controller.StopWalking();
    }
}