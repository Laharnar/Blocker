using UnityEngine;

public class RegisterClassToPrefab : MonoBehaviour {
    public bool saveOnStart = false;
    public MonoBehaviour toSave;
    public MonoBehaviourVar monoVar;

    private void Start()
    {
        Save();
    }

    public void Save()
    {
        monoVar.SetValue(toSave);
    }
}
