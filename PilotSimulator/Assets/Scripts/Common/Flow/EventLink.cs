using UnityEngine;
using UnityEngine.Events;

public class EventLink:Linked
{

    [SerializeField] bool used = true;
    [SerializeField] UnityEvent onTriggered;
    [SerializeField] bool log = false;

    public override void OverLink()
    {
        if (!used) return;
        if (log) Debug.Log("Link triggered." + name + " " + transform.root.name, this);
        onTriggered.Invoke();
    }
}