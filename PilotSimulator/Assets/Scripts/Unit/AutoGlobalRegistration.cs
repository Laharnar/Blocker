using UnityEngine;
// todo: remove
public class AutoGlobalRegistration:MonoBehaviour {
    private void Awake()
    {
        GlobalStorage.RegisterGlobally(transform, GetComponentInChildren<Alliance>());
        GlobalStorage.RegisterGlobally(transform, GetComponentInChildren<DamageReciever>());
        GlobalStorage.RegisterGlobally(transform, GetComponentInChildren<DamageSender>());
    }
}
