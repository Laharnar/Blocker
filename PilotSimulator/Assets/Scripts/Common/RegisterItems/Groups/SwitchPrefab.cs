using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Functions/Switch")]
public class SwitchPrefab : ScriptableObject {

    public DoEvent[] cases;

    protected void DoSwitch(int id)
    {
        cases[id].Do();
    }

    [System.Serializable]
    public class DoEvent {
        public DoPrefab onSuccess1;
        public UnityEvent onSuccess2;
        public bool skipBoth;

        public void Do()
        {
            if (skipBoth) return;
            onSuccess1.DoIt();
            onSuccess2?.Invoke();
        }
    }
}
