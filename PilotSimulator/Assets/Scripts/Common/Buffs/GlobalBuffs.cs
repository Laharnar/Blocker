using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalBuffs : MonoBehaviour, ITestable
{
    [Header("Untested how it works on hit.")]
    static Dictionary<UnitScriptLoader, Buff> buffs = new Dictionary<UnitScriptLoader, Buff>();

    public Transform buffsPrefab;

    static GlobalBuffs instance;
    public static GlobalBuffs Instance { get => instance; }

    public static void AddBuffOnUnit(int id, UnitScriptLoader unit)
    {
        if (Instance.buffsPrefab.childCount <= id)
        {
            Debug.LogError("Not enough buffs for requested id"+ id, Instance.buffsPrefab);
            return;
        }

        Transform obj = Instance.buffsPrefab.GetChild(id);
        Type prefItemType = obj.GetComponent<IPrefabLoadable>().GetType();
        Transform targetParent = unit.objLoader.GetItemGoal(prefItemType);
        Transform spawnedPrefabItem = Instantiate(obj, targetParent);
        
        buffs[unit] = spawnedPrefabItem.GetComponent<Buff>();
        buffs[unit].ConnectScript(unit);

        if (buffs[unit] == null)
        {
            Debug.LogError("No IPrefabLoadable found on prefab at child " + id, Instance.buffsPrefab);
            return;
        }
    }

    public void TestInitialState()
    {
        RealtimeTester.Assert(buffsPrefab != null, this, "Buffs prefab is null.");
    }
}