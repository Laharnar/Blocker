using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AttackEnemies: MonoBehaviour, ITactic
{
    public bool used = false;
    public CombatController combatController;

    private void Update() { if(used) Simulate(); }

    public void Activate() { used = true; }

    public void Deactivate() { 
        used = false;
        combatController.Stop();
    }

    public void Simulate()
    {
        if (!used) return;

        CombatController c0b = combatController;

        CombatUser enemyToFollow = c0b.SearchEnemy();
        c0b.Follow(enemyToFollow);

        if (enemyToFollow)
        {
            if (Vector3.Distance(enemyToFollow.transform.position, transform.position) < c0b.AttackRange)
            {
                c0b.AttackEnemy(enemyToFollow);
            }
        }
    }
}
