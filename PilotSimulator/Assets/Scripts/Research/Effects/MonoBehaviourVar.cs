using UnityEngine;

[CreateAssetMenu]
public class MonoBehaviourVar:ScriptableObject {

    public MonoBehaviour value;
    public ResearchTree ValueAsRt { get => (ResearchTree)Value; }

    public MonoBehaviourTypes expectType;

    public object Value {
        get {
            if (IsCorrectType())
                return value;
            return null;
        }
    }

    public bool IsCorrectType()
    {
        Debug.Log(value.GetType().Name + " "+ expectType.ToString());
        return value.GetType().Name == expectType.ToString();
    }

    public enum MonoBehaviourTypes {
        ResearchTree
    }
}
