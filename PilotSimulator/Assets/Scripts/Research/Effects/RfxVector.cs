using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Operations/Vectors")]
public class RfxVector:ScienceEffect {

    public float lastRan;
    public Vec3VarRef start;
    [SerializeField] Vector3 logStart;
    [Space] [Space]

    [Header("vector")]
    public BoolVarValue useX;
    public BoolVarValue useY;
    public BoolVarValue useZ;
    [Header("modificatios")]
    public ReverseVector reversing;
    public bool noneWereUsed = true;
    public Vec3VarRef ifNoneUsedFalse;
    [Space]
    public Vec3ArrayRef vectorOperations;
    public OperatorTransitional op;

    [Space] [Space]

    public Vec3VarRef result;
    [SerializeField] Vector3 logResult;

    protected override void Effect(ScienceArgs item)
    {
        lastRan = Time.time;
        // keeps old value, and adds through operation new values.
        Vector3 vector = start.Value;
        logStart = start.Value;
        for (int i = 0; i < vectorOperations.Count; i++)
        {
            if (useX.Value)
            {
                vector.x = TwoValueOperation.Operate(op.Value, vector.x, vectorOperations[i].x);
            }
            if (useY.Value)
            {
                vector.y = TwoValueOperation.Operate(op.Value, vector.y, vectorOperations[i].y);
            }
            if (useZ.Value)
            {
                vector.z = TwoValueOperation.Operate(op.Value, vector.z, vectorOperations[i].z);
            }
            vector = reversing.Reverse(vector);
        }

        if (useX.Value || useY.Value || useZ.Value)
        {
            noneWereUsed = false;
            result.Value = vector;
        }
        else
        {
            noneWereUsed = true;
            result.Value = ifNoneUsedFalse.Value;
        }
        logResult = result.Value;
    }
}
