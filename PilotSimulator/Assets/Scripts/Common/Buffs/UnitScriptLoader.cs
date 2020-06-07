using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitScriptLoader : MonoBehaviour
{
    public Health hp;
    public TransformTypeLoader objLoader;

    private void Awake()
    {
        objLoader.Init();
    }

    [Serializable]
    public class TransformTypeLoader
    {

        public Transform buffs;
        Dictionary<Type, Transform> itemGoals = new Dictionary<Type, Transform>();

        public void Init()
        {
            itemGoals.Add(typeof(Buff), buffs);
        }

        internal Transform GetItemGoal(Type prefItemType)
        {
            if (itemGoals.ContainsKey(prefItemType))
                return itemGoals[prefItemType];
            return null;
        }
    }
}
