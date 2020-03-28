using UnityEngine;

public class Alliance:MonoBehaviour {
    public int thisAlliance;
    public DamageSender writeBack;

    public void AllianceSame(object other)
    {
        DamageReciever o = (DamageReciever)other;
        writeBack.pass = thisAlliance == o.alliance.thisAlliance;
    }

    public void AllianceDifferent(object other)
    {
        DamageReciever o = (DamageReciever)other;
        writeBack.pass = thisAlliance != o.alliance.thisAlliance;
    }
    public void AllianceSame(DamageReciever other)
    {
        writeBack.pass = thisAlliance == other.alliance.thisAlliance;
    }
    public void AllianceDifferent(DamageReciever other)
    {
        writeBack.pass = thisAlliance != other.alliance.thisAlliance;
    }
    public void AllianceSame()
    {
        writeBack.pass = thisAlliance == writeBack.LastDamageOther.alliance.thisAlliance;
    }
    public void AllianceDifferent()
    {
        writeBack.pass = thisAlliance != writeBack.LastDamageOther.alliance.thisAlliance;
    }
}
