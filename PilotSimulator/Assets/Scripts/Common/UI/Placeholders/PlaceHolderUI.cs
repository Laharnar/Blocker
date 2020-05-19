using ICSharpCode.NRefactory.Visitors;
using System;
using UnityEngine;


public class PlaceHolderUI:MonoBehaviour, ITestable
{
    public bool isOn = true;

    public PlaceHolderView contentController;

    public void On(ResponseToClick useCase)
    {
        contentController.ResponseHandler(useCase);
    }

    public void Off(ResponseToClick useCase)
    {
        throw new NotImplementedException();
    }

    public void TestInitialState()
    {
        RealtimeTester.AssertSceneReference(contentController, this);
    }
}
