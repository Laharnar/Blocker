
using System.Collections.Generic;
using UnityEngine;
public class PositionRotation : ScienceAffected {

    public FloatVarRef moveSpeed, rotationSpeed;
    public Vector3 direction;
    public Vector3 rotationDeg;
    public Vector3 scaling = Vector3.one;
    public Vector3 lastRot;

    [Header("Special case")]
    public SpaceVarValue relativeTo;

    // Update is called once per frame
    void Update()
    {
        Vector3 moveAmount = direction * moveSpeed.Value * Time.deltaTime;
        Vector3 rotsAmount = rotationDeg * Time.deltaTime * rotationSpeed.Value;

        if (ScienceTree != null)
        {
            ScienceTree.args.moveX = moveAmount.x;
            ScienceTree.args.rotationAnglesY = rotsAmount.y;
            ScienceTree.args.source = transform;
            ScienceEffect.RequestScienceEffect(Effects, ScienceTree.args);
            // is this delayed? is it static?
            moveAmount = ScienceTree.args.moveDir;//new Vector3(0, 0, ArgsRef.args.moveX);
            rotsAmount.y *= ScienceTree.args.rotationDirY; // * direction * strength
        }
        
        transform.Translate(moveAmount, relativeTo.Value);

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
