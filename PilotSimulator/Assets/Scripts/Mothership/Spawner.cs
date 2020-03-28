using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner:MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float rate = 1;
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
        StartCoroutine(Spawn());
    }

    public void SetRate(float value)
    {
        rate = Mathf.Max(value, 0.00001f);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            SpawnNew(spawnPoint.position, spawnPoint.rotation);

            yield return new WaitForSeconds(rate);
        }
    }

    public void SpawnNew(Vector3 pos, Quaternion rot)
    {
        Transform source = Instantiate(target, pos, rot);
        spawned[this].Add(source);

        onSpawn.ActivateCall(source);
    }
}