
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PositionRotation : MonoBehaviour {

    public FloatVarRef moveSpeed, rotationSpeed;
    public Vector3 direction;
    public Vector3 rotationDeg;
    public Vector3 scaling = Vector3.one;
    public Vector3 lastRot;
    public bool useDeltaTime = true;

    [Header("Modifications")]
    public Vec3VarRef expectedMove;
    public Vec3VarRef expectedRotation;
    public UnityEvent onPreMove;

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
        ForcePositionRotationTick();
    }

    public void ForcePositionRotationTick()
    {
        if (rig == null || relativeTo.Value == Space.World)
        {
            CalculateBasicMoveRotateScale();
            ModifyDirectionAndRotationOnExtrenalScripts();
            MoveRotateScale();
        }
    }

    private void FixedUpdate()
    {
        ForceFixedUpdateTick();
    }

    private void ForceFixedUpdateTick()
    {
        if (rig != null && relativeTo.Value == Space.Self)
        {
            CalculateBasicMoveRotateScaleFixedUpdate();
            MoveRotateScale();
        }
    }

    private void CalculateBasicMoveRotateScale()
    {
        moveAmount = direction * moveSpeed.Value * (useDeltaTime ? Time.deltaTime : 1f);
        rotsAmount = rotationDeg * (useDeltaTime ? Time.deltaTime : 1f) * rotationSpeed.Value;
    }

    private void CalculateBasicMoveRotateScaleFixedUpdate()
    {
        moveAmount = direction * moveSpeed.Value * Time.fixedDeltaTime;
        rotsAmount = rotationDeg * Time.fixedDeltaTime * rotationSpeed.Value;
    }

    private void ModifyDirectionAndRotationOnExtrenalScripts()
    {
        expectedMove.Value = moveAmount;
        expectedRotation.Value = rotsAmount;
        onPreMove?.Invoke();
        moveAmount = expectedMove.Value;
        rotsAmount = expectedRotation.Value;
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
