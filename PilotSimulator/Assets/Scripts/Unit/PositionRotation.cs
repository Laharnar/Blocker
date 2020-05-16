
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PositionRotation : MonoBehaviour, ITestable {


    [Header("Parameters")]
    public FloatVarRef moveSpeed;
    public FloatVarRef rotationSpeed;
    public bool useDeltaTime = true;
    public bool needsRigidbody = true;
    public bool lockRotation;
    public SpaceVarValue relativeTo;

    [Header("Modifications")]
    public UnityEvent postPrediction;
    public Vec3VarRef expectedMove;
    public Vec3VarRef expectedRotation;

    public bool normalize = false;
    public bool deltaTime = false;
    public bool fixedDelta = false;
    public bool applySpeed = false;


    [Header("Instance")]
    public Transform targeted;
    public Rigidlink rigbody;

    [Header("Logs")]
    [SerializeField] private Vector3 prePredictionMove;
    [SerializeField] private Vector3 postPredictionMove;
    [SerializeField] private Vector3 postPredictionAngles;
    [SerializeField] Vector3 lastRot;


    [Header("Special case")]
    private Vector3 moveAmount;
    private Vector3 rotsAmount;

    [Header("Testing")]
    public bool test = true;
    public Vector3 direction;
    public Vector3 rotationDeg;
    public Vector3 scaling = Vector3.one;

    public Vector3 TransformPos { get => targeted.position; }

    private void Start()
    {
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
        if (rigbody.IsNull() || relativeTo.Value == Space.World)
        {
            TestTransformationValues();
            ApplyDeltaTime();
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
        if (!rigbody.IsNull() && relativeTo.Value == Space.Self)
        {
            TestFixedPhysicsValues();
            ApplyFixedDeltaTime();
            ModifyDirectionAndRotationOnExtrenalScripts();
            MoveRotateScale();
        }
    }

    private void TestTransformationValues()
    {
        moveAmount = direction * moveSpeed.Value;
        rotsAmount = rotationDeg  * rotationSpeed.Value;
    }

    void ApplySpeed()
    {
        moveAmount *= moveSpeed.Value;
        rotsAmount *= rotationSpeed.Value;
    }

    void ApplyDeltaTime()
    {
        moveAmount *= (useDeltaTime ? Time.deltaTime : 1f);
        rotsAmount *= (useDeltaTime ? Time.deltaTime : 1f);
    }
    void ApplyFixedDeltaTime()
    {
        moveAmount *= Time.fixedDeltaTime;
        rotsAmount *= Time.fixedDeltaTime;
    }

    private void TestFixedPhysicsValues()
    {
        moveAmount = direction * moveSpeed.Value;
        rotsAmount = rotationDeg * rotationSpeed.Value;
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

        if (normalize)
            moveAmount= moveAmount.normalized;

        if (deltaTime)
            ApplyDeltaTime();
        if (fixedDelta)
            ApplyFixedDeltaTime();
        if(applySpeed)
            ApplySpeed();
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
        if (rigbody.IsNull() || relativeTo.Value == Space.World)
        {
            targeted.Translate(moveAmount, relativeTo.Value);
            if (!lockRotation)
            {
                lastRot = transform.forward;
                targeted.Rotate(rotsAmount);
            }
        }
        else if (!rigbody.IsNull() && relativeTo.Value == Space.Self)
        {
            rigbody.MoveBy(transform.TransformDirection(moveAmount));
            if (!lockRotation)
                rigbody.RotateBy(rotsAmount);
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
                rigbody.RunTests();
            RealtimeTester.Assert(this != null, this, "Transfrom to move isn't assigned.");
        }
        RealtimeTester.Assert(moveSpeed.Value != 0, this, "Zero movement speed.");

        if (test)
        {
            RealtimeTester.Assert(direction != Vector3.zero, this, "Zero movement direction.");
            if (!lockRotation)
                RealtimeTester.Assert(rotationDeg != Vector3.zero && rotationSpeed.Value != 0, this, "Zero rotation parameters rotationDeg or rotationSpeed with unlocked rotation.");
        }


    }
    internal void IncreaseMovementSpeed(float moreSpeed)
    {
        moveSpeed.Value += moreSpeed;
    }

}
