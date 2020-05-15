using UnityEngine;

public class ControllerUI:MonoBehaviour, ITestable
{
    public PlaceHolderUI targetUI;
    public UICode useCase;

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
