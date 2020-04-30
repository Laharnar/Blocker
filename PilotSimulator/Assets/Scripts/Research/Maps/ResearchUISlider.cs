using UnityEngine;
using UnityEngine.UI;

public class ResearchUISlider : MonoBehaviour {

    public Slider slider;
    [SerializeField] FloatVar val;

    public float Value {
        get {
            val.Value = slider.value / 100f;
            return val.Value;
        }
    }

    public void Init(FloatVar customVal)
    {
        val = customVal;
    }

    public void OnSliderValuesChanges()
    {
        val.Value = slider.value / 100f;
    }
}
