using UnityEngine;
using UnityEngine.UI;

public class SpriteToImageUI : MonoBehaviour
{
    public Transform prefab;
    public UIId targetUI;
    public Image image;

    [ContextMenu("Reload")]
    void ReloadUI()
    {
        // Assumed that there are enough weapons to get their id.
        Transform target = prefab.GetChild(targetUI.id);
        if (!target)
        {
            Debug.LogError("Id is too large for amount of stored weapons. id: " + targetUI.id + " max: " + prefab.childCount);
            return;
        }
        SpriteRenderer spriteOnTarget = target.GetComponent<SpriteRenderer>();
        image.sprite = spriteOnTarget.sprite;
    }
}
