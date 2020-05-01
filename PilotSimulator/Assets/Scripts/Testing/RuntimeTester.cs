using UnityEngine;

public class RuntimeTester:MonoBehaviour
{
    public RealtimeTester tester;
    private void Start()
    {
        tester.RunExternally();
    }
    private void Update()
    {
        tester.RunExternally();
    }
}
