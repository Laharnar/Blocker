using UnityEngine;

public abstract class CombatTrigger:MonoBehaviour, ICombatTrigger
{
    public abstract void Trigger(GameObject other, string code);
}


public interface ICombatTrigger
{
    void Trigger(GameObject other, string code);
}
