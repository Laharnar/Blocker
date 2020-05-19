using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[System.Obsolete("Use other collision system.")]
public class OnHit : MonoBehaviour, ITestable
{

    public DamageSender sender;
    public IntVarValue selfAlliance;

    [SerializeField] RealtimePrefabs onHitRealtime;

    public ConditionGroup onHitIf;
    public UnityEvent onHit;

    [SerializeField] bool log = false;

    public void TestInitialState()
    {
        AttemptSetup();

        RealtimeTester.Assert(selfAlliance != null, this, "Missing selfAlliance");
        RealtimeTester.Assert(onHitRealtime, this, "Using obsolete settings");
        if (onHitRealtime)
        {
            RealtimeTester.Assert(onHitRealtime.onHitSaveSelfAllianceInto != null, this, "Missing onHitSaveSelfAllianceInto");
            RealtimeTester.Assert(onHitRealtime.onHitSaveOtherAllianceInto != null, this, "Missing onHitSaveOtherAllianceInto");
        }
    }

    void Start()
    {
        AttemptSetup();
    }
    private void AttemptSetup()
    {
        if (onHitRealtime == null && ObjectNotDestroyed())
            onHitRealtime = GetComponent<RealtimePrefabs>();
    }

    bool ObjectNotDestroyed()
    {
        return this != null;
    }

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
        string logging = "";
        if (log) logging = "CollisionEvt: " + name + " -> " + collision.name;
        HitEnter(collision, logging);
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
        HitEnter(collider, logging);
    }

    private void HitEnter(GameObject collider, string logging)
    {
        DamageReciever o = collider.GetComponent<DamageReciever>();
        if (o)
        {
            onHitRealtime.onHitSaveSelfAllianceInto.value = selfAlliance.Value;
            onHitRealtime.onHitSaveOtherAllianceInto.value = o.selfAlliance.Value;
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
