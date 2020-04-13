
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rig == null || relativeTo.Value == Space.World)
        {
            CalculateBasicMoveRotateScale();
            ApplyExternalModifications();
            MoveRotateScale();
        }
    }

    private void FixedUpdate()
    {
        if (rig != null && relativeTo.Value == Space.Self)
        {
            CalculateBasicMoveRotateScaleFixed();
            ApplyExternalModifications();
            MoveRotateScale();
        }
    }

    private void CalculateBasicMoveRotateScale()
    {
        moveAmount = direction * moveSpeed.Value * Time.deltaTime;
        rotsAmount = rotationDeg * Time.deltaTime * rotationSpeed.Value;
    }

    private void CalculateBasicMoveRotateScaleFixed()
    {
        moveAmount = direction * moveSpeed.Value * Time.fixedDeltaTime;
        rotsAmount = rotationDeg * Time.fixedDeltaTime * rotationSpeed.Value;
    }
    private void MoveRotateScale()
    {
        if (rig == null || relativeTo.Value == Space.World)
        {
            transform.Translate(moveAmount, relativeTo.Value);
            lastRot = transform.forward;
            transform.Rotate(rotsAmount);
        }
        else if (rig != null && relativeTo.Value == Space.Self)
        {
            rig.MovePosition(rig.position + transform.InverseTransformDirection(moveAmount));
            rig.MoveRotation( Quaternion.Euler(rig.rotation.eulerAngles+rotsAmount));
        }
        else
        {
            Debug.LogError("Missing rigidbody.", this);
        }
        if(transform.localScale != scaling)
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
