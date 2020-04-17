using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IntUI : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public IntVar optionalInt;

    private void Update()
    {
        if(optionalInt!= null)
            SetValue(optionalInt);
    }

    private void SetValue(IntVar i)
    {
        text.SetText(i.value.ToString());
    }

    public void SetValue(int x)
    {
        text.SetText(""+x);
    }

    public void SetValue(Slider slider)
    {
        text.SetText(slider.value.ToString());
    }
}
