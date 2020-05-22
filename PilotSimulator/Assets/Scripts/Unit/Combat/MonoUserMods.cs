using UnityEngine;

public class MonoUserMods:MonoBehaviour
{
    [SerializeField] float logSum = 0;
    protected IUserMods userMods;

    public virtual void InitMods(IUserMods userMods)
    {
        this.userMods = userMods;
        userMods.OnModGetsNewValue += ModAdded;
    }

    public virtual void ModAdded(float modValue)
    {
    }

    public float GetSum()
    {
        if(userMods != null)
            logSum = userMods.GetModSum();
        logSum = 0;
        return logSum;
    }
}
