using UnityEngine.SocialPlatforms;

[System.Serializable]
public class AttackAction
{
    public int damage;
    public AttackAction(int damage)
    {
        this.damage = damage;
    }

    public int GetDamage(IUserMods mods)
    {
        return damage + (int)mods.GetModSum();
    }
}
