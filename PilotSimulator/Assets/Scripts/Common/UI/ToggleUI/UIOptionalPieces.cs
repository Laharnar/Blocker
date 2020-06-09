
[System.Serializable]
public class UIOptionalPieces
{
    public ImageUI image;
    public SpriteUI sprite;

    public void SetColor(int col)
    {
        image.SetColor(col);
        sprite.SetColor(col);
    }
}
