using UnityEngine;

public abstract class Observer:MonoBehaviour
{
    public abstract void Notified(float value);
}
