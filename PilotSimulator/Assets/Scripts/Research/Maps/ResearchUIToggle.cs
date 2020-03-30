using UnityEngine;
using UnityEngine.UI;

public class ResearchUIToggle : MonoBehaviour {

    public Toggle toggle;
    [SerializeField] BoolVar val;

    public void Init(BoolVar customVal)
    {
        Debug.Log("This is used [*1]. if not, remove it.");
        val = customVal;
    }

    public void OnToggleValuesChanges()
    {
        val.value = toggle.isOn;
    }
}