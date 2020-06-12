using System;
using UnityEngine;

public class RallyToPoint : MonoBehaviour, ITactic
{
    [SerializeField] bool used = false;
    [SerializeField] CombatController combatController;
    [SerializeField] float searchRate = 5;
    [SerializeField] float enemySearchRange=5;
    [SerializeField] float rallyToRange = 0.5f;
    [SerializeField] CombatUser enemy;

    bool searchRally;
    bool arrived;
    float letThemRunAway;

    private void Update() { if (used) Simulate(); }

    public void Activate() { used = true; }

    public void Deactivate()
    {
        used = false;
        combatController.Stop();
    }

    public void Simulate()
    {
        if (!used) return;
        CombatController c0b = combatController;

        // reset every 3 seconds, to allow enemy to escape the area.
        if (Time.time > letThemRunAway)
        {
            searchRally = true;
        }
        else
        {
            searchRally = false;
        }
        if (searchRally)
        {
            Target goTo = RallyTargets.Closest(c0b.transform.position);
            if (goTo != null)
            {
                arrived = GoNear(combatController, goTo.target, rallyToRange);
                if (arrived)
                {
                    enemy = null;
                    letThemRunAway = Time.time + searchRate;
                    searchRally = false;
                }
            }
        }
        else
        {
            if (enemy == null)
            {
                enemy = SearchAndLockNearbyEnemy(c0b, enemySearchRange);
                letThemRunAway = Time.time + searchRate;
            }
            if (c0b.IsInAttackRange(enemy))
            {
                c0b.AttackEnemy(enemy);
                c0b.Stop();
            }
        }
    }

    private CombatUser SearchAndLockNearbyEnemy(CombatController combatant, float searchRange)
    {
        CombatUser enemyToFollow = combatant.SearchEnemy();
        float dist = Vector3.Distance(enemyToFollow.transform.position, transform.position);
        bool nearEnemy = dist < searchRange;
        if (nearEnemy)
        {
            combatant.OffensiveLocking(enemyToFollow);
        }
        return enemyToFollow;
    }

    private bool GoNear(CombatController c0b, Vector3 target, float range)
    {
        // true when arrive.
        if(Vector3.Distance(target, c0b.transform.position) > range)
        {
            c0b.Travel(target);
            return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if(searchRally)
            GizmoDraw.Circle(transform.position, rallyToRange, Color.blue);
        else
            GizmoDraw.Circle(transform.position, enemySearchRange, Color.red);
    }
}

public static class GizmoDraw
{
    public static void Circle(Vector2 pos, float radius)
    {
        Gizmos.DrawWireSphere(pos, radius);
    }
    public static void Circle(Vector2 pos, float radius, Color c)
    {
        Gizmos.color = c;
        Gizmos.DrawWireSphere(pos, radius);
    }
}
