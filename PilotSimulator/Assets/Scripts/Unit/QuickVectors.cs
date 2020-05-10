using UnityEngine;

public class QuickVectors:MonoBehaviour
{
    public Vec3ArrayRef vectors;
    public MovementPlanning target;

    public void RemoveAt(int i)
    {
        target.vectors.RemoveAt(i);
    }

    public void InsertTo0(int id)
    {
        target.vectors.Insert(0, vectors[id]);
    }

    public void InsertTo1(int id)
    {
        target.vectors.Insert(1, vectors[id]);
    }

    public void InsertTo2(int id)
    {
        target.vectors.Insert(2, vectors[id]);
    }

    public void OverwriteTargetAsFirst(int id)
    {
        target.vectors[0] = vectors[id];
    }

    public void OverwriteTargetAsSecond(int id)
    {
        target.vectors[1] = vectors[id];
    }

    public void OverwriteTargetAsThird(int id)
    {
        target.vectors[2] = vectors[id];
    }

    public void AddTargetAsEnd(int id)
    {
        target.vectors.Add(vectors[id]);
    }
}
