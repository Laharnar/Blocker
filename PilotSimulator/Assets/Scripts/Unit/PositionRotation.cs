
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

    private Vector3 moveAmount;
    private Vector3 rotsAmount;

    // Update is called once per frame
    void Update()
    {
        CalculateBasicMoveRotateScale();
        ApplyExternalModifications();
        MoveRotateScale();
    }

    private void CalculateBasicMoveRotateScale()
    {
        moveAmount = direction * moveSpeed.Value * Time.deltaTime;
        rotsAmount = rotationDeg * Time.deltaTime * rotationSpeed.Value;
    }

    private void MoveRotateScale()
    {
        transform.Translate(moveAmount, relativeTo.Value);

        lastRot = transform.forward;
        transform.Rotate(rotsAmount);

        transform.localScale = scaling;
    }

    private void ApplyExternalModifications()
    {
        if (ScienceTree != null)
        {
            ScienceTree.args.source = transform;
            ScienceEffect.RequestScienceEffect(Effects, ScienceTree.args);
            moveAmount = ScienceTree.args.moveDir;
            rotsAmount.y *= ScienceTree.args.rotationDirY;
        }
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
