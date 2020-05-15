﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] TransformVarValue prefab;
    [SerializeField] TransformVarValue spawnPoint;

    static Dictionary<Spawner, List<Transform>> spawned = new Dictionary<Spawner, List<Transform>>();
    [SerializeField] AutoLinker linkerForSpawned;


    void Awake()
    {
        spawned.Add(this, new List<Transform>());
    }

    // Event usable.
    public void SpawnNewAtSpawnPoint()
    {
        SpawnNew(spawnPoint.Value.position, spawnPoint.Value.rotation);
    }

    public void SpawnNew(Vector3 pos, Quaternion rot)
    {
        if (spawned.ContainsKey(this))
        {
            Transform spawnedNew = Instantiate(prefab.Value, pos, rot);
            spawned[this].Add(spawnedNew);

            linkerForSpawned.SetupLink(spawnedNew);
        }
        else
        {
            Debug.LogError("Issue when trying to spawn. Key with this spawner doesn't exist in static global dictionary.", this);
        }
    }
}
