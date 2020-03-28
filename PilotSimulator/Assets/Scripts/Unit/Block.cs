using System.Collections.Generic;
using UnityEngine;
public class Block : ScienceAffected {
    public FloatVarRef moveSpeed, rotationSpeed;
    public Vector3 direction;
    public Vector3 rotationDeg;
    public Vector3 scaling = Vector3.one;
    public Vector3 lastRot;
    // Update is called once per frame
    void Update()
    {
        Vector3 moveAmount = direction * moveSpeed.Value * Time.deltaTime;
        Vector3 rotsAmount = rotationDeg * Time.deltaTime * rotationSpeed.Value;

        if (ArgsRef != null)
        {
            ArgsRef.args.moveX = moveAmount.x;
            ArgsRef.args.rotationAnglesY = rotsAmount.y;
            ArgsRef.args.source = transform;
            ScienceEffect.RequestScienceEffect(Effects, ArgsRef.args);
            // is this delayed? is it static?
            moveAmount = new Vector3(0, 0, ArgsRef.args.moveX);
            rotsAmount.y *= ArgsRef.args.rotationDirY; // * direction * strength
        }
        transform.Translate(moveAmount, Space.Self);
        lastRot = transform.forward;
        transform.Rotate(rotsAmount);


        transform.localScale = scaling;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, lastRot);
    }

    public void SetMovement(Vector3 dir)
    {
        direction = dir;
    }
}
