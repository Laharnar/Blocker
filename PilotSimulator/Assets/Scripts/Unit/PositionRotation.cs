
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PositionRotation : MonoBehaviour, ITestable {

    public FloatVarRef moveSpeed, rotationSpeed;
    public Vector3 direction;
    public Vector3 rotationDeg;
    public Vector3 scaling = Vector3.one;
    public Vector3 lastRot;
    public bool useDeltaTime = true;
    public bool needsRigidbody = true;
    public Transform targeted;
    public Rigidbody rig;

    [Header("Modifications")]
    public Vec3VarRef expectedMove;
    public Vec3VarRef expectedRotation;

    public UnityEvent postPrediction;
    [SerializeField] private Vector3 prePredictionMove;
    [SerializeField] private Vector3 postPredictionMove;
    [SerializeField] private Vector3 postPredictionAngles;


    [Header("Special case")]
    public SpaceVarValue relativeTo;

    private Vector3 moveAmount;
    private Vector3 rotsAmount;

    private void Start()
    {
        if(rig== null) rig = GetComponent<Rigidbody>();
        expectedMove.Value = Vector3.zero;
        if (targeted == null) targeted = transform;
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
            ModifyDirectionAndRotationOnExtrenalScripts();
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
        prePredictionMove = moveAmount;
        postPrediction?.Invoke();
        postPredictionMove = expectedMove.Value;
        postPredictionAngles = expectedRotation.Value;
        moveAmount = expectedMove.Value;
        rotsAmount = expectedRotation.Value;
    }

    public void SetMoveValue(Vec3Var vec3)
    {
        expectedMove.Value = vec3.value;
    }

    public void SetRotationValue(Vec3Var rot3)
    {
        expectedRotation.Value = rot3.value;
    }

    private void MoveRotateScale()
    {
        if (rig == null || relativeTo.Value == Space.World)
        {
            targeted.Translate(moveAmount, relativeTo.Value);
            lastRot = transform.forward;
            targeted.Rotate(rotsAmount);
        }
        else if (rig != null && relativeTo.Value == Space.Self)
        {
            rig.MovePosition(rig.position + transform.TransformDirection(moveAmount));
            rig.MoveRotation( Quaternion.Euler(rig.rotation.eulerAngles+rotsAmount));
        }
        else
        {
            Debug.LogError("Missing rigidbody.", this);
        }
        if(targeted.localScale != scaling)
            targeted.localScale = scaling;
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

    public void TestInitialState()
    {
        if (Application.isPlaying)
        {
            if(needsRigidbody)
                RealtimeTester.Assert(rig != null, this, "Rigidbody isn't assigned.");
            RealtimeTester.Assert(transform != null, this, "Transfrom to move isn't assigned.");
        }
    }
}
