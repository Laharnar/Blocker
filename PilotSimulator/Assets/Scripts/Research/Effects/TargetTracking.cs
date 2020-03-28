using UnityEngine;

[System.Serializable]
public class TargetTracking {

    [SerializeField] Vector3 pos;
    [SerializeField] Transform target;
    [SerializeField] bool _objectless;
    public bool objectless { get => _objectless; private set => _objectless = value; }

    public Vector3 Position { get {
            if (objectless)
            {
                return pos;
            }
            return target.position;
        }
    }

    public TargetTracking(Vector3 pos)
    {
        this.pos = pos;
        objectless = true;
    }
    public TargetTracking(Transform t)
    {
        target = t;
        objectless = false;
    }
}
