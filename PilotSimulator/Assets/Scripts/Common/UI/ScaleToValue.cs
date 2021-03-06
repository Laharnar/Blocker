﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToValue : MonoBehaviour
{
    public Transform scaled;
    public MonoConnection valueGetterCurrent;
    public MonoConnection valueGetterMax;

    // Update is called once per frame
    void Update()
    {
        float current = valueGetterCurrent.ValueGetter.GetValue(0);
        float max = valueGetterMax.ValueGetter.GetValue(1);
        if (max == 0)
            current = 0;
        scaled.localScale = new Vector3(current / max+0.001f, 1, 1);
    }
}
