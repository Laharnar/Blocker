using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour {

    public ReferenceCall call = new ReferenceCall()
    {
        callFun = "OnHit"
    };

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision "+name +" -> "+collision.gameObject.name);
        DamageReciever o = collision.gameObject.GetComponent<DamageReciever>();
        if(o)
            call.ActivateCall(o);
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger " + name + " -> " + collider.gameObject.name);
        DamageReciever o = collider.gameObject.GetComponent<DamageReciever>();
        if (o)
            call.ActivateCall(o);
    }
}
