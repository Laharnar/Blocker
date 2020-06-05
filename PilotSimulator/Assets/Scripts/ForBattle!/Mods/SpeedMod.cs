public class SpeedMod : StatMods
{

    public PositionRotation rotationSpeed;

    public override void Notified(IModData modValue)
    {
        rotationSpeed.rotationSpeed.Value += modValue.ModValue;
    }
}
