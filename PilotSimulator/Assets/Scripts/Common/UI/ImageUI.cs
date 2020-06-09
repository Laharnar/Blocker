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

[System.Serializable]
public class SpriteUI
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color[] colors;

    public void SetColor(int col)
    {
        if(sprite && col <colors.Length)
            sprite.color = colors[col];
    }
}
