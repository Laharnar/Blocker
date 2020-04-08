using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner:MonoBehaviour {
    [SerializeField] TransformVarValue target;
    [SerializeField] ProgrammableDelay rate;
    [SerializeField] TransformVarValue spawnPoint;

    static Dictionary<Spawner, List<Transform>> spawned = new Dictionary<Spawner, List<Transform>>();

    void Start()
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
        Transform source = Instantiate(target.Value, pos, rot);
        spawned[this].Add(source);
    }
}