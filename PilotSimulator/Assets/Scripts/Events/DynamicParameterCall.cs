using System;
using UnityEngine;

// System to share a custom object over multiple preselected references
[System.Serializable]
public class DynamicParameterCall {
    public string callFun = "None";
    public string[] callFuns = new string[0];
    public MonoBehaviour[] references;
    public bool use = true;

    internal void ReassignReferencesTo(Transform spawned)
    {
        for (int i = 0; i < references.Length; i++)
        {

        }
    }

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