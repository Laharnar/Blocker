using UnityEngine;

public class DefendBase : MonoBehaviour, ITactic, ITestable
{
    public bool used = false;
    public CombatController combatant;

    //public Rect areaAroundBase;

    public float structureApproachingRange;
    public float searchRate;
    public float searchRange;
    
    [SerializeField] bool returningToBase = false;
    [SerializeField] TacticalUnit unit;

    [Header("|Realtime|")]
    float letThemRunAway;
    [SerializeField] CombatUser enemyToFollow;

    [Header("|Log|")]
    public Transform logBaseTarget;

    [SerializeField]
    Transform DefendedBase {
        get {
            return logBaseTarget = unit.AllyBaseToDefend;
        }
    }

    private void Update() { 
        if (used) Simulate();
    }

    public void Activate() { used = true; }

    public void Deactivate()
    {
        used = false;
        combatant.Stop();
        OnLoseFocusTactic.Do(combatant);
    }

    public void Simulate()
    {
        if (!used) return;

        if (returningToBase)
        {
            DefensiveAroundBase(combatant);
        }
        else
        {
            SearchAndLockNearbyEnemy();
        }

        // reset every 3 seconds, to allow enemy to escape the area.
        if (Time.time > letThemRunAway)
        {
            ReturnNearBase();
        }
    }

    private void SearchAndLockNearbyEnemy()
    {
        enemyToFollow = combatant.SearchEnemy();
        float dist = Vector3.Distance(enemyToFollow.transform.position, transform.position);
        bool nearEnemy = dist < searchRange;
        if (nearEnemy)
        {
            combatant.OffensiveLocking(enemyToFollow);
        }
    }

    private void DefensiveAroundBase(CombatController combatant)
    {
        Transform defended = DefendedBase;
        if (defended)
        {
            float dist = Vector3.Distance(defended.position, transform.position);
            bool nearStructure = dist < structureApproachingRange;
            if (nearStructure)
            {
                combatant.Stop();
                letThemRunAway = Time.time + searchRate;
                returningToBase = false;
            }
            else
            {
                combatant.Travel(defended.position);
            }
        }
        else
        {
            combatant.Stop();
        }
    }

    void ReturnNearBase()
    {
        returningToBase = true;
        if(enemyToFollow)
            combatant.ReleaseEnemy(enemyToFollow);
    }

    private void OnDestroy()
    {
        if (enemyToFollow)
            combatant.ReleaseEnemy(enemyToFollow);
    }

    public void TestInitialState()
    {
        RealtimeTester.Assert(structureApproachingRange > 0, this, "ApproachStructureRange is <0, expect >0.");
        RealtimeTester.Assert(searchRate > 0, this, "ApproachStructureRange is <0. expect >0");
    }
}
