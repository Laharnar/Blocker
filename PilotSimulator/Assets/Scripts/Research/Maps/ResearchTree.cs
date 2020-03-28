using UnityEngine;
using UnityEngine.Events;

public class ResearchTree:MonoBehaviour {

    public ResearchTreePackage science1;

    public int researchPoints = 0;// VSP


    public UnityEvent onBuyAny;

    public UnityEvent OnRecievedPositiveResearch;
    public MonoBehaviourVar saveIntoPrefab;
    public IntUI researchPointsUI;

    public EventItem[] uiControllers;

    private void Awake()
    {
        saveIntoPrefab.value = this;
        saveIntoPrefab.expectType = MonoBehaviourVar.MonoBehaviourTypes.ResearchTree;
    }

    private void Update()
    {
        // for testing 
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            RecieveResearchPoints(10);
        }
    }

    public void RecieveResearchPoints(int pts)
    {
        researchPoints += pts;

        UpdateResearchPointsUI();

        // when adding points externall, also invoke this. a bit messy.
        // ignored when buying.
        if(pts > 0)
            OnRecievedPositiveResearch?.Invoke();
    }

    private void UpdateResearchPointsUI()
    {
        if (researchPointsUI) researchPointsUI.SetValue(researchPoints);

    }

    public void BuyIfHasMoney(int upgradeId)
    {
        if (science1.Buy(upgradeId, researchPoints))
        {
            RecieveResearchPoints(-science1.researchOptions[upgradeId].cost.value);
            uiControllers[upgradeId]?.UnsafeCall("Buy");
            onBuyAny?.Invoke();
        }

    }

    public void AddVSP(int v)
    {
        RecieveResearchPoints(v);
    }

}
