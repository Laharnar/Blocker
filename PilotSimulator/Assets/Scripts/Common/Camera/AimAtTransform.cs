using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AimAtTransform : MonoBehaviour
{
    public TransformVarValue target;

    [Range(0.0f, 1.0f)]
    public float strength;

    public bool fullAimOnStart = false;

    public Vec3VarRef targetPositionOnStart;
    public FloatVarRef distanceToTargetAtStart;

    public UnityEvent onStartEnd;

    void Start()
    {
        if (target == null)
            return;

        if (fullAimOnStart)
        {
            transform.up = target.Value.position - transform.position;
        }

        targetPositionOnStart.Value = target.Value.position;
        distanceToTargetAtStart.Value = Vector2.Distance(target.Value.position, transform.position);
        onStartEnd?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

        if(target!= null)
        transform.up = Vector3.Lerp(transform.up, (target.Value.position - transform.position).normalized, strength);
    }
}
