using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchMapsDisplay : MonoBehaviour
{
    public Transform markerPrefab;
    private Transform mapParent => transform;
    private ResearchCluster cluster;// multiple bases can connect.
    private List<ResearchResult> displayData = new List<ResearchResult>();
    private List<Transform> displayedObjects = new List<Transform>();
    public bool on = false;
    public DisplayControllerBase[] controllers;

    private void Start()
    {
        SetMapVisbility(on);
    }

    public void SetMapVisbility(bool value)
    {
        on = value; // allows ui to call this function.
        mapParent.gameObject.SetActive(value);
        if (value)
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                controllers[i].EnableVisuals();
            }
        }
        else
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                controllers[i].DisableVisuals();
            }
        }
    }

    public void ToggleMap()
    {
        on = !on;
        SetMapVisbility(on);
    }

    public void DisplayUI(ResearchResult results)
    {
        displayData.Add(results);
        Instantiate(markerPrefab, results.lastImportantPoint + results.recordedRelativePosition, new Quaternion(), mapParent);
    }

    // linked to ui.
    public void SetSourceCluster(ResearchCluster cluster)
    {
        if (cluster) cluster.onRecieveResearch -= DisplayUI;
        this.cluster = cluster;
        if (this.cluster == null) Debug.LogError("ASsigned null cluster.");
        if (cluster) cluster.onRecieveResearch += DisplayUI;
    }
}

public abstract class DisplayControllerBase : MonoBehaviour {
    internal abstract void DisableVisuals();

    internal abstract void EnableVisuals();
}
