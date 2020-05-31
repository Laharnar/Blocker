using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    static Dictionary<Spawner, List<Transform>> spawned = new Dictionary<Spawner, List<Transform>>();

    [Header("Spawning")]
    [SerializeField] TransformVarValue prefab;
    [SerializeField] TransformVarValue spawnPoint;
    [SerializeField] float delayBeforeSpawning = 0;

    [Header("Linking")]
    [SerializeField] AutoLinker linkerForSpawned;
    [SerializeField] bool log = false;

    void Awake()
    {
        spawned.Add(this, new List<Transform>());
    }

    public void ScheduleNewAfterDelay()
    {
        Invoke("SpawnNewAtSpawnPoint", delayBeforeSpawning);
    }

    // Event usable.
    public void SpawnNewAtSpawnPoint()
    {
        if (log) Debug.Log("Spawner:Spawn at spawn point");
        SpawnNew(spawnPoint.Value.position, spawnPoint.Value.rotation);
    }

    private void SpawnNew(Vector3 pos, Quaternion rot)
    {
        if (spawned.ContainsKey(this))
        {
            Transform spawnedNew = Instantiate(prefab.Value, pos, rot);
            spawned[this].Add(spawnedNew);

            linkerForSpawned.SetupLink(spawnedNew);
        }
        else
        {
            Debug.LogError("Spawner:Issue when trying to spawn. Key with this spawner doesn't exist in static global dictionary.", this);
        }
    }
}
