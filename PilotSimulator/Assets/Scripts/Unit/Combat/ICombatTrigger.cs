using UnityEngine;

public interface ICombatTrigger
{
    void Trigger(GameObject colliderTrigger, string code);
}
