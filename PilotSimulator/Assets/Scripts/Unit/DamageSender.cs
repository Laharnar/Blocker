using UnityEngine;
using UnityEngine.Events;

public class DamageSender : MonoBehaviour {

    public int damage;

    // todo: remove these 3
    public UnityEvent onDamage;
    public bool pass = true;
    public DamageReciever LastDamageOther { get; private set; }

    public void OnHit(DamageReciever d)
    {
        // todo: remove these 4
        LastDamageOther = d;
        onDamage?.Invoke();
        if (pass)
        {

            d.hp.RecieveDamage(damage);

            // todo: remove these 2
            pass = false;
        }
    }
}
