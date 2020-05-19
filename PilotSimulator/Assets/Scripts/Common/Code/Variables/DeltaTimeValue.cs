using System.CodeDom;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/DeltaTime")]
public class DeltaTimeValue : FloatVar
{
    public new float Value
    {
        get {
            base.Value = Time.deltaTime;
            return Time.deltaTime; }
        set { base.Value = Time.deltaTime; }
    }
}
