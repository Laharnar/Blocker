using System;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour, ITestable {
    public IntVarValue health;
    public IntVarValue maxHealth;
    public UnityEvent OnDamaged;
    public UnityEvent OnDestroyed;

    public bool selfDestroy = true;
    public bool destroyed = false;
    public Transform destroyTarget;
    public bool checkEveryFrameToCoverPrefabChanges => !health.useDefault;


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
        if (dmg < 0)
        {
            Debug.Log("Recieve damage. "+dmg);
        }
        else if(dmg > 0)
        {
            Debug.Log("Recieve healing. "+dmg);
        }
        else
        {
            Debug.Log("Damage negated.");
        }

        health.Value = Mathf.Clamp(health.Value - dmg, 0, maxHealth.Value);
        OnDamaged.Invoke();
        if (health.Value <= 0)
        {
            DestroyFromHp();
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
