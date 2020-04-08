using UnityEngine;
using UnityEngine.Events;

public class DamageSender : MonoBehaviour {

    public int damage;

    public void OnHit(DamageReciever d)
    {
        d.hp.RecieveDamage(damage);
    }
}
