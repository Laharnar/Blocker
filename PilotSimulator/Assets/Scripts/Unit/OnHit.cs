using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnHit : MonoBehaviour {

    public DamageSender sender;
    public IntVarValue selfAlliance;

    public IntVar onHitSaveSelfAllianceInto;
    public IntVar onHitSaveOtherAllianceInto;
    public RealtimePrefabs onHitRealtime;

    public ConditionGroup onHitIf;
    public UnityEvent onHit;

    public bool log = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollideEnter(collision.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        CollideEnter(collision.gameObject);
    }

    private void CollideEnter(GameObject collision)
    {
        if (log) Debug.Log("Collision " + name + " -> " + collision.name);
        DamageReciever o = collision.GetComponent<DamageReciever>();
        if (o)
        {
            if (onHitRealtime)
            {
                onHitRealtime.onHitSaveSelfAllianceInto.value = selfAlliance.Value;
                onHitRealtime.onHitSaveOtherAllianceInto.value = o.selfAlliance.Value;
            }
            else
            {
                Debug.LogWarning("obsolete, use realtime isntead.");
                onHitSaveSelfAllianceInto.value = selfAlliance.Value;
                onHitSaveOtherAllianceInto.value = o.selfAlliance.Value;
            }
            if (onHitIf.IsTrue())
            {
                onHit?.Invoke();
                sender.OnHit(o);
            }
        }
    }

    protected void OnTriggerEnter(Collider collider)
    {
        TriggerEnter(collider.gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        TriggerEnter(collider.gameObject);
    }


    private void TriggerEnter(GameObject collider)
    {
        string logging = "";
        if (log) logging = "TriggerEvt: " + name + " -> " + collider.name;

        DamageReciever o = collider.GetComponent<DamageReciever>();
        if (o)
        {
            if (onHitRealtime)
            {
                onHitRealtime.onHitSaveSelfAllianceInto.value = selfAlliance.Value;
                onHitRealtime.onHitSaveOtherAllianceInto.value = o.selfAlliance.Value;
            }
            else
            {
                Debug.LogWarning("obsolete, use realtime isntead.");
                onHitSaveSelfAllianceInto.value = selfAlliance.Value;
                onHitSaveOtherAllianceInto.value = o.selfAlliance.Value;
            }
            if (onHitIf.IsTrue())
            {
                onHit?.Invoke();
                sender.OnHit(o);
                if (log) logging += " - Success";
            }
            else if (log) logging += " - Failure";
        }
        else if (log) logging += " - No DamageReciever.";
        if (log) Debug.Log(logging);
    }
}
