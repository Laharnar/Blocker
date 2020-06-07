using UnityEngine;

public abstract class Buff:CombatScript, IPrefabLoadable
{
    [SerializeField] protected UnitScriptLoader scripts;

    [SerializeField] private float fireRate;

    [SerializeField] private float passedTime = 0;

    protected abstract void OnBuffTick();

    public void ConnectScript(UnitScriptLoader scripts)
    {
        this.scripts = scripts;
    }

    public bool IsReadyPausable()
    {
        if (scripts == null) return false;

        // When it's not called, time won't be added, which makes it pausable.
        passedTime += Time.deltaTime;

        if (passedTime > fireRate)
        {
            return true;
        }
        return false;
    }

    public void Next()
    {
        if (passedTime > fireRate)
            passedTime -= fireRate;
    }

    protected override void CombatUpdate()
    {
        if (IsReadyPausable())
        {
            Next();
            OnBuffTick();
        }
    }

}
