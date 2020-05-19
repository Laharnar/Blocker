using UnityEngine;

public class UpgradeSetter : MonoBehaviour
{
    public SimpleUpgrades[] upgrades;
    public SimpleUpgrades[] setTo;

    void Start()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].FullReset(setTo[i]);
        }
    }
}