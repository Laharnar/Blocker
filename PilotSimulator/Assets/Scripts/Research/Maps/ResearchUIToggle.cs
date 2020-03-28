using UnityEngine;
using UnityEngine.UI;

public class ResearchUIToggle : MonoBehaviour {

    public Toggle toggle;
    [SerializeField] BoolVar val;

    public void Init(BoolVar customVal)
    {
        val = customVal;
    }

    public void OnToggleValuesChanges()
    {
        val.value = toggle.isOn;
    }
}