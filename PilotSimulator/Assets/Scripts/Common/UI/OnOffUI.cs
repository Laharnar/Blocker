using UnityEngine;

public class OnOffUI:MonoBehaviour, ITestable
{
    public PlaceHolderUI targetUI;
    public ResponseToClick useCase;

    public void On()
    {
        targetUI.On(useCase);
    }
    public void Off()
    {
        targetUI.Off(useCase);
    }

    public void TestInitialState()
    {
        RealtimeTester.Assert(EmptyReference.IsNotEmpty(targetUI), this, "Missing targetUI on ControllerUI.");
        EmptyReference.Initializer(this, ref targetUI);
    }
}
