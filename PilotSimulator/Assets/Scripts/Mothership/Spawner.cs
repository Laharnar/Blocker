using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner:MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] ProgrammableDelay rate;
    [SerializeField] Transform spawnPoint;

    public ReferenceCall onSpawn = new ReferenceCall()
    {
        callFun = "NewSpawn",
        use = false
    };

    static Dictionary<Spawner, List<Transform>> spawned = new Dictionary<Spawner, List<Transform>>();

    void Start()
    {
        spawned.Add(this, new List<Transform>());
    }

    // Event usable.
    public void SpawnNewAtSpawnPoint()
    {
        SpawnNew(spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnNew(Vector3 pos, Quaternion rot)
    {
        Transform source = Instantiate(target, pos, rot);
        spawned[this].Add(source);

        onSpawn.ActivateCall(source);
    }
}