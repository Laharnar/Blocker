using UnityEngine;

[System.Serializable]
public class ReferenceCall {
    public string callFun = "None";
    public string[] callFuns =new string[0];
    public MonoBehaviour[] references;
    public bool use = true;

    public void ActivateCall(object c)
    {
        if (!use) return;
        if (callFuns.Length != references.Length)
        {
            for (int i = 0; i < references.Length; i++)
            {
                references[i].SendMessage(callFun, c);
            }
        }
        else
        {
            for (int i = 0; i < callFuns.Length; i++)
            {
                references[i].SendMessage(callFuns[i], c);
            }
        }
    }
}