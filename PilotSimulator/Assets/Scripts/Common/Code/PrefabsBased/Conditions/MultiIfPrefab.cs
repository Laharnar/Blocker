using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Functions/MultiIf")]
public class MultiIfPrefab : ScriptableObject {

    public IfThenItem[] ifs;
    public DoEvent allFail;

    public void DoMultiIf()
    {
        try
        {
            for (int i = 0; i < ifs.Length; i++)
            {
                if (ifs[i].IsTrue())
                {
                    ifs[i].Do();
                    return;
                }
            }
            allFail.Do();
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogException(e, this);
            throw;
        }
    }

    [System.Serializable]
    public class IfThenItem {
        public string context;
        public ConditionGroup condition;
        public bool autoTrue = false;
        public bool autoFalse = false;

        public DoEvent onSuccess;

        public bool IsTrue()
        {
            if (autoTrue) return true;
            if (autoFalse) return false;
            return condition.IsTrue();
        }

        public bool IsFalse()
        {
            if (autoTrue) return true;
            if (autoFalse) return false;
            return condition.IsFalse();
        }

        public void Do()
        {
            onSuccess.Do();
        }
    }

    [System.Serializable]
    public class DoEvent {
        public bool skipBoth;
        public DoPrefab onSuccess1;
        public UnityEvent onSuccess2;

        public void Do()
        {
            if (skipBoth) return;
            if(onSuccess1) onSuccess1.DoIt();
            onSuccess2?.Invoke();
        }
    }
}
