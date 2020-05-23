public class SpeedMod : StatMods
{

    public PositionRotation rotationSpeed;

    public override void Notified(float modValue)
    {
        rotationSpeed.rotationSpeed.Value += modValue;
    }
}
