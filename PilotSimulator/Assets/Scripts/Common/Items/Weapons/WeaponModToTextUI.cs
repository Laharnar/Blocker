using TMPro;
using UnityEngine;

public class WeaponModToTextUI : MonoBehaviour
{
    public Transform weaponPrefab;
    public UIId targetUI;
    public TextMeshProUGUI textUI;

    [ContextMenu("Reload")]
    void ReloadUI()
    {
        // Assumed that there are enough weapons to get their id.
        Transform weapon = weaponPrefab.GetChild(targetUI.id);
        if (!weapon) { 
            Debug.LogError("Id is too large for amount of stored weapons. id: " + targetUI.id + " max: " + weaponPrefab.childCount);
            return;
        }
        BonusList bonusOnWeapon = weapon.GetComponent<BonusList>();
        textUI.text = bonusOnWeapon.ToUI();
    }
}
