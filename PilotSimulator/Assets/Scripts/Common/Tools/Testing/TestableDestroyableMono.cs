using UnityEngine;

public abstract class TestableDestroyableMono:MonoBehaviour, ITestable
{
    public abstract void TestInitialState();

    protected virtual void OnDestroy()
    {
        RealtimeTester.DestroyedTestableObject(this);
    }
}
