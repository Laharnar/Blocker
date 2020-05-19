using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Functions/Do")]
public class DoPrefab:ScriptableObject {

    public bool skip = false;
    public UnityEvent doAction;

    public void DoIt()
    {
        if (skip) return;

        doAction?.Invoke();
    }
}
