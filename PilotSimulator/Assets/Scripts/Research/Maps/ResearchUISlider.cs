using UnityEngine;
using UnityEngine.UI;

public class ResearchUISlider : MonoBehaviour {

    public Slider slider;
    [SerializeField] FloatVar val;

    public float Value {
        get {
            val.value = slider.value / 100f;
            return val.value;
        }
    }

    public void Init(FloatVar customVal)
    {
        val = customVal;
    }

    public void OnSliderValuesChanges()
    {
        val.value = slider.value / 100f;
    }
}
