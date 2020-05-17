using UnityEngine;

public class MonoUserMods:MonoBehaviour
{
    protected IUserMods userMods;

    public virtual void InitMods(IUserMods userMods)
    {
        this.userMods = userMods;
        userMods.OnModGetsNewValue += ModAdded;
    }

    public virtual void ModAdded(float modValue)
    {
    }
}
