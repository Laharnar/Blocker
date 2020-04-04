using UnityEngine;

public class IntVarInitializer : MonoBehaviour {
    public IntVar[] vars;
    public int[] initValues;

    private void Awake()
    {
        for (int i = 0; i < initValues.Length && i < vars.Length; i++)
        {
            vars[i].value = initValues[i];
        }
    }
}
