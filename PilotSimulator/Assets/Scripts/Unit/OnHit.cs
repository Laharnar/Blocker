using System;
using System.Collections;
using UnityEngine;

public class OnHit : MonoBehaviour {

    public DamageSender sender;
    public IntVarValue selfAlliance;

    public IntVar onHitSelfAlliance;
    public IntVar onHitOtherAlliance;
    public ConditionGroup onHitIf;

    public bool log = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(log)Debug.Log("Collision "+name +" -> "+collision.gameObject.name);
        DamageReciever o = collision.gameObject.GetComponent<DamageReciever>();
        if (o)
        {
            onHitSelfAlliance.value = selfAlliance.Value;
            onHitOtherAlliance.value = o.selfAlliance.Value;
            if (onHitIf.IsTrue())
                sender.OnHit(o);
        }
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (log) Debug.Log("Trigger " + name + " -> " + collider.gameObject.name);
        DamageReciever o = collider.gameObject.GetComponent<DamageReciever>();
        if (o)
        {
            onHitSelfAlliance.value = selfAlliance.Value;
            onHitOtherAlliance.value = o.selfAlliance.Value;
            if (onHitIf.IsTrue())
                sender.OnHit(o);
        }
    }
}
