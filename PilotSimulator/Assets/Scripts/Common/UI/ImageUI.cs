using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageUI
{
    [SerializeField] Image image;
    [SerializeField] Color[] colors;

    public void SetColor(int col)
    {
        if(image && col < colors.Length)
            image.color = colors[col];
    }
}

