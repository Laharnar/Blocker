using UnityEngine;

public abstract class ListenersObserver : MonoBehaviour
{ 
    // this allows some specific behaviours to be triggered from listeners to observers.
    public abstract void Notified(IModData value);
}
