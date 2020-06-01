using System.Linq;
using UnityEngine;

public class TacticalBases:MonoBehaviour
{
    // Tells which tactics or global targets should the commanded units go for.

    public TacticGroup group;

    [Header("Realtime | Commanding bases")]
    [SerializeField] Transform defendingBase;
    [SerializeField] public CombatUser enemyBoss;

    private void Update()
    {

        LoadDefendedBase(defendingBase);
        LoadAttackableBase(enemyBoss);
    }

    private void LoadAttackableBase(CombatUser enemyBoss)
    {
        for (int i = 0; i < group.units.Count; i++)
        {
            group.units[i].SetEnemyBoss(enemyBoss);
        }
    }

    private void LoadDefendedBase(Transform defendingBase)
    {
        for (int i = 0; i < group.units.Count; i++)
        {
            group.units[i].SetAllyBase(defendingBase);
        }
    }
}
