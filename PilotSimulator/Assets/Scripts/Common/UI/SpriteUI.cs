using UnityEngine;

public class SpriteUI:MonoBehaviour, IUIContextItem, ISetupUnity
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color[] colors;
    [SerializeField] UIGroupedContext optionalContext;

    private void OnEnable() => optionalContext?.Register(this);
    private void OnDisable() => optionalContext?.Unegister(this);

    public void SetColor(int col)
    {
        if(sprite && col < colors.Length)
            sprite.color = colors[col];
    }

    public void ResetUI()
    {
        SetColor(0);
    }

    public void ResetContextAndSetColor(int id)
    {
        optionalContext?.ResetContextUI();
        SetColor(id);
    }

    public bool UnitySetup()
    {
        if (optionalContext == null)
        {
            optionalContext = GetComponentInParent<UIGroupedContext>();
            enabled = false;
            enabled = true;
        }
        return (optionalContext != null);
    }
}

