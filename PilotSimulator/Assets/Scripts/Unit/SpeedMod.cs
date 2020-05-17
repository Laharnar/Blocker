public class SpeedMod : MonoUserMods
{

    public PositionRotation rotationSpeed;

    public override void ModAdded(float modValue)
    {
        rotationSpeed.rotationSpeed.Value += modValue;
    }
}
