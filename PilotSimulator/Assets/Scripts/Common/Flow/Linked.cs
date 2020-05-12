using UnityEngine;
using UnityEngine.Events;

public class Linked : MonoBehaviour
{
    [SerializeField] bool used = true;
    [SerializeField] UnityEvent onTriggered;
    [SerializeField] bool log = false;

    public virtual void TriggerLink()
    {
        if (!used) return;
        if (log) Debug.Log("Link triggered."+name+" "+transform.root.name, this);
        onTriggered.Invoke();
    }

    public void LinkTo(Linker linker)
    {
        linker.Connect(this);
    }

    public void UnlinkFrom(Linker linker)
    {
        linker.Disconnect(this);
    }
}
