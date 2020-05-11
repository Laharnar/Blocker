using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public CombatUser self;

    public SpriteRenderer sprite;
    public bool attackingLocked = false;

    public CombatCollisionTrigger attackRange;
    public AttackAction basicAttackDamage = new AttackAction(1);
    public float attackLength = 0.1f;
    public float waitBetweenAttacks = 2f;
    public int searchForEnemyId = 0;

    // Update is called once per frame
    void Update()
    {
        if (attackingLocked) return;
        Attack();
    }

    private void Attack()
    {

        if (attackRange.collisions.Count > 0)
        {
            CombatUser[] all = GameObject.FindObjectsOfType<CombatUser>();
            CombatUser[] enemies = GetEnemyUnits(all, searchForEnemyId);
            CombatUser enemy = Closest(enemies);

            if (!enemy)
            {
                Debug.LogError("Enemy count is 0 when searching for " + searchForEnemyId, this);
            }
            else
            {
                StartCoroutine(AttackRoutine(enemy));
            }
        }
    }

    private static CombatUser[] GetEnemyUnits(CombatUser[] mixed, int enemyId)
    {
        List<CombatUser> enemies = new List<CombatUser>();
        for (int i = 0; i < mixed.Length; i++)
        {
            if(mixed[i].alliance.thisAlliance == enemyId)
            {
                enemies.Add(mixed[i]);
            }
        }
        return enemies.ToArray();
    }

    private CombatUser Closest(CombatUser[] users)
    {
        CombatUser closest = null;
        float minDist = float.MaxValue;
        for (int i = 0; i < users.Length; i++)
        {
            if (users[i] == self) continue;

            float dist = Vector3.Distance(users[i].transform.position, self.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = users[i];
            }
        }
        return closest;
    }

    private IEnumerator AttackRoutine(CombatUser attacked)
    {
        attackingLocked = true;

        attacked.Damage(basicAttackDamage);
        sprite.color = Color.red;
        yield return new WaitForSeconds(attackLength);
        sprite.color = Color.white;

        yield return new WaitForSeconds(waitBetweenAttacks);
        attackingLocked = false;
    }
}
