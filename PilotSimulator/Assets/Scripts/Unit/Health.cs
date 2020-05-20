using System;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour, ITestable, IHealth
{
    public IntVarValue health;
    public IntVarValue maxHealth;
    [SerializeField] UnityEvent OnDamaged;
    [SerializeField] UnityEvent OnDestroyed;

    public bool selfDestroy = true;
    public bool destroyed = false;
    public Transform destroyTarget;
    public bool checkEveryFrameToCoverPrefabChanges => !health.useDefault;
    [SerializeField] bool logDamage = false;
    [SerializeField] bool logDeath = false;

    public int Hp {
        get {
            return health.Value;
        }
    }

    public int MaxHp {
        get {
            return maxHealth.Value;
        }
    }

    private void Start()
    {
        if (maxHealth.Value == 0)
        {
            Debug.LogError("Max hp is 0."+name, this);
        }
        health.Value = maxHealth.Value;
    }

    public void RecieveDamage(int dmg)
    {
        LogDmg(dmg);
        health.Value = Mathf.Clamp(Hp - dmg, 0, MaxHp);
        OnDamaged.Invoke();
        if (health.Value <= 0)
        {
            LogDeath();
            DestroyFromHp();
        }
    }

    private void LogDeath()
    {
        Debug.Log("Unit died "+transform.root.name);
    }

    private void LogDmg(int dmg)
    {
        if (logDamage)
        {
            if (dmg > 0)
            {
                Debug.Log("Recieve damage. " + dmg, this);
            }
            else if (dmg < 0)
            {
                Debug.Log("Recieve healing. " + dmg, this);
            }
            else
            {
                Debug.Log("Damage negated.", this);
            }
        }
    }

    private void DestroyFromHp()
    {
        OnDestroyed.Invoke();
        if (selfDestroy)
        {
            destroyed = true;
            Destroy(gameObject);
        }
        else
        {
            destroyed = true;
            Destroy(destroyTarget.gameObject);
        }
    }

    private void Update() // this covers other sources of damage.
    {
        if (checkEveryFrameToCoverPrefabChanges)
        {
            if (health.Value <= 0)
            {
                DestroyFromHp();
            }
        }
    }

    public void TestInitialState()
    {
        if(!selfDestroy && !destroyed)
            RealtimeTester.Assert(destroyTarget != null, this, "Source DestroyTarget isn't assigned to Health.");
    }
}
