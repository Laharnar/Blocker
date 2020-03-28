using UnityEngine;
using UnityEngine.Events;

public class DamageSender : MonoBehaviour {

    public int damage;

    public UnityEvent onDamage;
    public bool pass = true;
    public DamageReciever LastDamageOther { get; private set; }

    public void OnHit(DamageReciever d)
    {
        LastDamageOther = d;
        onDamage?.Invoke();
        if (pass)
        {
            d.hp.RecieveDamage(damage);
            pass = false;
        }
    }
}
