using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnapToObject : MonoBehaviour
{
    public Transform obj;
    public List<Transform> others = new List<Transform>();
    public UnityEvent evt;

    public void SnapDestroyOthers(UIId id)
    {
        others[id.id].position = obj.position;
        for (int i = 0; i < others.Count; i++)
        {
            if(i != id.id)
                DestroyObj(others[i]);
        }
        evt.Invoke();
    }

    void DestroyObj(Transform t)
    {
        Destroy(t.gameObject);
    }
}
