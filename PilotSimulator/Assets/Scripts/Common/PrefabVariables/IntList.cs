using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lists/IntList")]
public class IntList : ScriptableObject {
    public List<IntVarValue> ints = new List<IntVarValue>();

}