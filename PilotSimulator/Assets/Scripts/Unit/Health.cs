using System;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour {
    public IntVarValue health;
    public IntVarValue maxHealth;
    public UnityEvent OnDamaged;
    public UnityEvent OnDestroyed;
    public bool checkEveryFrame = true;
    bool destroyedFromHealth = false;

    private void Start()
    {
        if (maxHealth.Value == 0)
        {
            Debug.LogError("Max hp is 0."+name, this);
        }
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

        Debug.Log(name + " recieve damage "+dmg);
        health.Value = Mathf.Clamp(health.Value - dmg, 0, maxHealth.Value);
        OnDamaged.Invoke();
        if (health.Value <= 0)
        {
            destroyedFromHealth = true;
            OnDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    private void Update() // this covers other sources of damage.
    {
        if (checkEveryFrame)
        {
            if (health.Value <= 0)
            {
                destroyedFromHealth = true;
                OnDestroyed.Invoke();
                Destroy(gameObject);
            }
        }
    }

}
