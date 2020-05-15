using ICSharpCode.NRefactory.Visitors;
using System;
using UnityEngine;


public class PlaceHolderUI:MonoBehaviour, ITestable
{
    public bool isOn = true;

    public PlaceHolderView contentController;

    public void On(UICode useCase)
    {
        contentController.Interact(useCase);
    }

    public void Off(UICode useCase)
    {
        throw new NotImplementedException();
    }

    public void TestInitialState()
    {
        RealtimeTester.AssertSceneReference(contentController, this);
    }
}
