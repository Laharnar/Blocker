using System;
using UnityEngine;

public class CombatCollisionTrigger : MonoBehaviour, ICombatTrigger
{
    [SerializeField]CollisionsInfo collisions;

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
        Trigger(collision.gameObject, "In2d");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Trigger(collision.gameObject, "In3d");
    }

    protected void OnTriggerEnter(Collider collider)
    {
        Trigger(collider.gameObject, "On2d");
    }
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Trigger(collider.gameObject, "On3d");
    }
    protected void OnTriggerExit2D(Collider2D collider)
    {
        collisions.TryRemove(collider.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        collisions.TryRemove(other.gameObject);
    }

    public void Trigger(GameObject collider, string code)
    {
        collisions.Add(new CollisionsInfo.CollisionInfo()
        {
            start = gameObject,
            otherCollision = collider,
            otherRoot = collider.GetComponent<CombatCollisionTrigger>()?.root,
            code = code
        }) ;
    }
}
