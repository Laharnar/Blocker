using UnityEngine;

[CreateAssetMenu]
public class RfxTracking: ScienceEffect {

    public bool use = true;
    public BoolVar trackingUnlocked;
    public FloatVar trackingRotationWeight;
    public FloatVar allyDeathsWeight;
    public FloatVar pickClosestWeight;

    public AnimationCurve rotationDamping;// 1:180 degrees, 0: 0 degrees


    protected override void Effect(ScienceArgs args)
    {
        if (!use)
            return;

        // include tracking
        if (!trackingUnlocked.value)
        {
            return;
        }

        if (args.knownAllyDeaths!= null && args.knownAllyDeaths.Count > 0)
        {
            // TODO: first instead of closest.
            Vector3 pos = Vector3.zero;
            pos += (args.knownAllyDeaths[0].lastImportantPoint+ args.knownAllyDeaths[0].recordedRelativePosition) * allyDeathsWeight.Value * (1 - pickClosestWeight.Value);

            // find closest
            float minRange=float.MaxValue;
            int closest=0;
            float tempRange = 0;
            for (int i = 0; i < args.knownAllyDeaths.Count; i++)
            {
                tempRange = Vector3.Distance(args.knownAllyDeaths[i].recordedRelativePosition+ args.knownAllyDeaths[i].lastImportantPoint, args.Source.position);
                if (tempRange<minRange)
                {
                    closest = i;
                    minRange = tempRange;
                }
            }
            pos += (args.knownAllyDeaths[closest].lastImportantPoint + args.knownAllyDeaths[closest].recordedRelativePosition) * allyDeathsWeight.Value *  pickClosestWeight.Value;
            args.target = new TargetTracking(pos);

        }

        if (args.target != null)
        {
            // Pick by weighting originally requested rotation results vs tracking rotation.
            float original = args.rotationDirY;
            Vector3 samePlane1 = args.Source.forward;
            samePlane1.y = 0;
            Vector3 samePlane2 = args.target.Position - args.Source.position;
            samePlane2.y = 0;
            args.trackingAngleY = Vector3.SignedAngle(samePlane1, samePlane2, Vector3.up);

            float normalizedAngle =  args.trackingAngleY / 180;// 0 to 180
            args.rotationDirY = original * (1 - trackingRotationWeight.Value) + rotationDamping.Evaluate(normalizedAngle) * trackingRotationWeight.Value*Mathf.Sign(args.trackingAngleY);
        }
        else
        {
            Debug.Log("Tracking result is null.");
        }
    }
}
