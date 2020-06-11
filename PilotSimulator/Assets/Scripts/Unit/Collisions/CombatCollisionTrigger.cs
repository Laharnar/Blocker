using System;
using UnityEngine;

public class CombatCollisionTrigger : MonoBehaviour, ITestable
{
    [System.Serializable]
    public class OnCollisionTriggerConnection: ICombatTrigger
    {
        public CollisionsInfo collisions;
        public CombatTrigger[] traps;

        public void Trigger(GameObject otherRoot, string code)
        {
            for (int i = 0; i < traps.Length; i++)
            {
                traps[i].Trigger(otherRoot, code);
            }
        }
    }
    [SerializeField] bool log = false;
    [SerializeField] CollisionsInfo collisions;

    public OnCollisionTriggerConnection connections;
      
    public CircleCollider2D circleCollider;

    public GameObject root;
    float Range { get => circleCollider.radius; }

    public bool IsInRange(Transform t)
    {
        return Vector3.Distance(t.position, transform.position) < Range 
            || collisions.Contains(t.gameObject);// Sometimes, collisions can be too big, which doesn't allow units to get close.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collide(collision.gameObject, "In2d");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collide(collision.gameObject, "In3d");
    }

    protected void OnTriggerEnter(Collider collider)
    {
        Collide(collider.gameObject, "On2d");
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Collide(collider.gameObject, "On3d");
    }

    private void Collide(GameObject other, string code)
    {
        if(log)
            Debug.Log(gameObject +" colliding with "+other+" as "+ code, gameObject);
        CollisionInfo meta = SetupMeta(other, code);
        if (meta.otherRoot)
            connections.Trigger(meta.otherRoot, meta.code);
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        collisions.TryRemove(collider.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        collisions.TryRemove(other.gameObject);
    }

    CollisionInfo SetupMeta(GameObject collider, string code)
    {
        CombatCollisionTrigger trigger = collider.GetComponent<CombatCollisionTrigger>();
        if (!trigger)
        {
            Debug.Log("Other doesn't have collisiotn trigger(other is linked to this log). This can result in errors with scripts that need root.", collider);
        }
        Rigidbody2D rig = collider.GetComponent<Rigidbody2D>();
        GameObject root;
        if (rig)
            root = rig.gameObject;
        else root = trigger?.root;

        CollisionInfo info = new CollisionInfo()
        {
            start = gameObject,
            otherCollision = collider,
            otherRoot = root,
            code = code
        };
        collisions.Add(info);
        return info;
    }

    public void TestInitialState()
    {
        // porting to new version
        connections.collisions = this.collisions;
    }
}
