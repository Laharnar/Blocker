using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScienceArgs {
    // --- movement ---
    public Vector3 move;
    public float rotationAnglesY;

    // --- tracking ---
    public List<ResearchResult> knownAllyDeaths;

    // who is requesting science calculation
    public Transform source;

    /// <summary>
    /// Results from tracking.
    /// </summary>
    public TargetTracking target;

    /// <summary>
    /// Angle to target.
    /// </summary>
    public float trackingAngleY;
    public Vector3 trackedPosition;
    public float moveX;
    public float rotationDirY;
}