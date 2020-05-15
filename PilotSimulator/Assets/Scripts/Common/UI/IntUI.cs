using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class IntUI : MonoBehaviour
{
    public TMP_Text text;
    public IntVar optionalInt;
    public bool setEveryFrame = true;

    private void Update()
    {
        if(setEveryFrame)
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
