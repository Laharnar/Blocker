using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AttackEnemies: MonoBehaviour, ITactic
{
    [SerializeField] bool used = false;
    [SerializeField] CombatController combatController;
    [SerializeField] CombatUser enemyToFollow;

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

        enemyToFollow = c0b.SearchEnemy();
        c0b.Follow(enemyToFollow);

        
        if (c0b.IsInAttackRange(enemyToFollow))
        {
            c0b.AttackEnemy(enemyToFollow);
        }
    }
}
