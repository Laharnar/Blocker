
using UnityEngine;
public abstract class Linked : MonoBehaviour
{

    [SerializeField] Linker connectedTo;

    protected Linker ConnectedTo { get => connectedTo; }

    public abstract void OverLink();

    public void LinkTo(Linker linker)
    {
        if (connectedTo != null)
            UnlinkFrom(connectedTo);
        linker.Connect(this);
    }

    public void UnlinkFrom(Linker linker)
    {
        connectedTo = null;
        linker.Disconnect(this);
    }
}
