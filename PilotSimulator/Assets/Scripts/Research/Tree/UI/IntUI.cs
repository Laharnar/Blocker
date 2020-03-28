using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntUI : MonoBehaviour
{
    public TMPro.TMP_Text text;

    public void SetValue(int x)
    {
        text.SetText(""+x);
    }

    public void SetValue(Slider slider)
    {
        text.SetText(slider.value.ToString());
    }
}
