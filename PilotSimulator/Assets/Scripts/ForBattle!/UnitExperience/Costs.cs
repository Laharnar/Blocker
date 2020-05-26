using System.Collections.Generic;
using UnityEngine;

public class Costs:MonoBehaviour
{
    [SerializeField] List<int> costsPerLevel = new List<int>();

    public int GetCosts(int level)
    {
        return costsPerLevel[level];
    }
}
