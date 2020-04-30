using System.Collections.Generic;
using UnityEngine;

public class MultiSpawner : MonoBehaviour
{
    [SerializeField] TransformVarValue[] prefab;

    [SerializeField] TransformVarValue defaultSpawnPoint;
    [SerializeField] TransformVarValue[] spawnPoint;

    int toSpawn;

    // prefab/spawned
    static Dictionary<Transform, List<Transform>> spawned = new Dictionary<Transform, List<Transform>>();

    void Awake()
    {
        for (int i = 0; i < prefab.Length; i++)
        {
            spawned.Add(prefab[i].Value, new List<Transform>());
        }
    }

    public void SetActiveToSpawn(int id)
    {
        toSpawn = Mathf.Clamp(id, 0, prefab.Length-1);
    }

    // Event usable.
    public void SpawnNewAtSpawnPoint(int id)
    {
        toSpawn = id;
        SpawnNew(spawnPoint[toSpawn].Value.position, spawnPoint[toSpawn].Value.rotation);
    }

    private void SpawnNew(Vector3 pos, Quaternion rot)
    {
        Transform source = Instantiate(prefab[toSpawn].Value, pos, rot);
        spawned[prefab[toSpawn].Value].Add(source);
    }
}
