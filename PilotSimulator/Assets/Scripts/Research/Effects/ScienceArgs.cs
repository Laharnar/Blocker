using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScienceArgs {

    // --- tracking ---
    public List<ResearchUnitArgs> knownAllyDeaths;

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
    public float rotationDirY;
    public Vector3 moveDir;

    internal void StartOnNewObject()
    {
        // If this isn't called, some stuff can be left over from last object.
        // When 0 effects are applied those value stay, resulting in weird behaviour.
        trackingAngleY = 0;
        rotationDirY = 0;
        moveDir = Vector3.zero;
    }
}