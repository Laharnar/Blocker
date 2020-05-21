using UnityEngine;
using UnityEngine.Assertions;

public class MovementPlanning:MonoBehaviour, ITestable
{
    public Vec3ArrayRef vectors;
    public GroupOperation _operator;
    public PositionRotation user;
    public float minDistance=0.5f;

    public void ModifyMove()
    {
        Vector3 direction = _operator.Operate(vectors.VectorsCopy()) - user.TransformPos;
        if (direction.magnitude > minDistance)
            user.expectedMove.Value = direction;
    }

    public void InitVectors(int number)
    {
        for (int i = 0; i < number; i++)
        {
            vectors.Add(new Vector3());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, vectors[0]);
    }

    public void RemoveAt(int i)
    {
        vectors.RemoveAt(i);
    }

    public void InsertTargetAsFirst(Vector3 target)
    {
        vectors.Insert(0, target);
    }

    public void InsertTargetAsSecond(Vector3 target)
    {
        vectors.Insert(1, target);
    }

    public void InsertTargetAsThird(Vector3 target)
    {
        vectors.Insert(2, target);
    }

    public void OverwriteTargetAsFirst(Vector3 target)
    {
        vectors[0] = target;
    }

    public void OverwriteTargetAsSecond(Vector3 target)
    {
        vectors[1] = target;
    }

    public void OverwriteTargetAsThird(Vector3 target)
    {
        vectors[2] = target;
    }

    public void AddTargetAsEnd(Vector3 target)
    {
        vectors.Add(target);
    }

    public void TestInitialState()
    {
        RealtimeTester.AssertSceneReference(user, this);
    }
}
