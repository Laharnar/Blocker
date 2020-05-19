using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Functions/Switch")]
public class SwitchPrefab : ScriptableObject {

    public IntVarValue activeId;
    public DoEvent[] cases;

    public void DoSwitch()
    {
        DoSwitch(activeId.Value);
    }

    public void DoSwitch(int id)
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
            onSuccess1?.DoIt();
            onSuccess2?.Invoke();
        }
    }
}
