using System;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour, ITestable, IHealth
{
    [SerializeField] IntVarValue health;
    [SerializeField] IntVarValue maxHealth;
    [SerializeField] UnityEvent OnDamaged;
    [SerializeField] UnityEvent OnDestroyed;

    public bool selfDestroy = true;
    public bool destroyed = false;
    public Transform destroyTarget;

    public bool checkEveryFrameToCoverPrefabChanges => !health.useDefault;

    [SerializeField] bool logDamage = false;
    [SerializeField] bool logDeath = false;
    [SerializeField] bool triggerDamage1 = false;

    int startMaxHealth;
    int startHealth;
    int bonusMaxHp;


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

    private void Awake()
    {
        if (maxHealth.Value == 0)
        {
            Debug.LogError("Max hp is 0."+name, this);
        }
        health.Value = maxHealth.Value;
        startHealth = health.Value;
        startMaxHealth = maxHealth.Value;
        bonusMaxHp = 0;
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

    internal void SetBonusMaxHpAndRecalcHp(int modValue)
    {
        bonusMaxHp = modValue;
        int hpLost = maxHealth.Value - health.Value;
        int newMax = bonusMaxHp + startMaxHealth;
        int newCur = newMax - hpLost;
        health.Value = newCur;
        maxHealth.Value = newMax;
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
        if (triggerDamage1)
        {
            RecieveDamage(1);
            triggerDamage1 = false;
        }
    }

    public void TestInitialState()
    {
        if(!selfDestroy && !destroyed)
            RealtimeTester.Assert(destroyTarget != null, this, "Source DestroyTarget isn't assigned to Health.");
    }

}
