using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    public int health = 1;
    public UnityEvent OnDestroyed;
    bool destroyedFromHealth = false;
    public void RecieveDamage(int dmg)
    {
        health -= dmg;
        if (health<= 0)
        {
            destroyedFromHealth = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (destroyedFromHealth)
        {
            OnDestroyed.Invoke();
        }
    }
}
