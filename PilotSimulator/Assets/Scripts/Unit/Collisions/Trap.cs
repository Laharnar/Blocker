using UnityEngine;

public class Trap : CombatTrigger
{
    public int damage;
    public bool destroySelfOnHit = false;
    public Transform selfRoot;
    public override void Trigger(GameObject otherRoot, string code)
    {
        CombatUser otherCombatant = otherRoot.GetComponent<CombatUser>();
        if (otherCombatant)
        {
            otherCombatant.Damage(damage);
        }
        else
        {
            Debug.LogError("other doesn't have comabt user script.");
        }
        if (destroySelfOnHit)
            Destroy(selfRoot);
    }
}
