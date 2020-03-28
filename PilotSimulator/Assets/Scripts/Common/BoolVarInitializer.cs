using UnityEngine;

public class BoolVarInitializer:MonoBehaviour {
    public BoolVar[] vars;
    public bool[] initValues;

    private void Awake()
    {
        for (int i = 0; i < initValues.Length && i < vars.Length; i++)
        {
            vars[i].value = initValues[i];
        }
    }
}
