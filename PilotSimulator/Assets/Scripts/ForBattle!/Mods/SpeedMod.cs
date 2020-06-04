public class SpeedMod : StatMods
{

    public PositionRotation rotationSpeed;

    public override void Notified(UpgradeData modValue)
    {
        rotationSpeed.rotationSpeed.Value += modValue.increase;
    }
}
