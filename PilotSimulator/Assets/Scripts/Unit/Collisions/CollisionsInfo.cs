using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CollisionInfo
{
    public string code;
    public GameObject start;

    public GameObject otherCollision;
    public GameObject otherRoot;
}
public class CollisionsInfo:MonoBehaviour
{

    public int staticLogCount;
    [SerializeField]List<CollisionInfo> collisions = new List<CollisionInfo>();

    internal void Add(CollisionInfo collisionInfo)
    {
        collisions.Add(collisionInfo);
        staticLogCount = collisions.Count;
    }

    internal bool Contains(GameObject t)
    {
        for (int i = 0; i < collisions.Count; i++)
        {
            if (collisions[i].otherRoot == t || collisions[i].otherCollision == t)
                return true;
        }
        return false;
    }

    internal void TryRemove(GameObject t)
    {
        for (int i = collisions.Count - 1; i >= 0; i--)
        {
            if (collisions[i].otherRoot == t || collisions[i].otherCollision == t)
                collisions.RemoveAt(i);
        }
    }

}
