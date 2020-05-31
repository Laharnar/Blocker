using UnityEngine;

public class DefendBase : MonoBehaviour, ITactic, ITestable
{
    public bool used = false;
    public CombatController combatant;

    //public Rect areaAroundBase;

    public float ApproachStructureRange;
    public float SearchAroundTime;
    public float maxAllowedSearchRange;
    
    [SerializeField] bool returningToBase = false;
    [SerializeField] TacticGroup unit;

    [SerializeField] Transform DefendBaseTarget {
        get {
            return logBaseTarget = unit.AllyBaseToDefend;
        }
    }
    [Header("|Realtime|")]
    [SerializeField] float letThemRunAway;
    [SerializeField] CombatUser enemyToFollow;

    [Header("|Log|")]
    public Transform logBaseTarget;

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
            enemyToFollow = combatant.SearchEnemy();
            float dist = Vector3.Distance(enemyToFollow.transform.position, transform.position);
            bool nearEnemy = dist < maxAllowedSearchRange;
            if (nearEnemy)
            {
                combatant.OffensiveLocking(enemyToFollow);
            }
        }

        // reset every 3 seconds, to allow enemy to escape the area.
        if (Time.time > letThemRunAway)
        {
            ReturnNearBase();
        }
    }


    private void DefensiveAroundBase(CombatController combatant)
    {
        Transform defended = DefendBaseTarget;
        if (defended)
        {
            float dist = Vector3.Distance(defended.position, transform.position);
            bool nearStructure = dist < ApproachStructureRange;
            if (nearStructure)
            {
                combatant.Stop();
                letThemRunAway = Time.time + SearchAroundTime;
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
        RealtimeTester.Assert(ApproachStructureRange > 0, this, "ApproachStructureRange is <0, expect >0.");
        RealtimeTester.Assert(SearchAroundTime > 0, this, "ApproachStructureRange is <0. expect >0");
    }
}
