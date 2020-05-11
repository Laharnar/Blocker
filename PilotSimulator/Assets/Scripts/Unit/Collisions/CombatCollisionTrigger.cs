using System.Collections.Generic;
using UnityEngine;

public class CombatCollisionTrigger : MonoBehaviour, ICombatTrigger
{

    public int staticLogCount;

    public class CollisionInfo
    {
        public string code;
        public GameObject start;
        public GameObject target;
    }

    public List<CollisionInfo> collisions = new List<CollisionInfo>();

    private void Update()
    {
        staticLogCount = collisions.Count;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Trigger(collision.gameObject, "In2");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Trigger(collision.gameObject, "In3");
    }

    protected void OnTriggerEnter(Collider collider)
    {
        Trigger(collider.gameObject, "On2");
    }
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Trigger(collider.gameObject, "On3");
    }

    public void Trigger(GameObject collider, string code)
    {
        collisions.Add(new CollisionInfo()
        {
            start = gameObject,
            target = collider,
            code = code
        }) ;
    }
}
