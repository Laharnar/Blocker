using UnityEngine;

public class FloatVarInitializer : MonoBehaviour {
    public FloatVar[] vars;
    public float[] initValues;

    private void Awake()
    {
        for (int i = 0; i < initValues.Length && i < vars.Length; i++)
        {
            vars[i].Value = initValues[i];
        }
    }
}
