using System.Collections.Generic;
using UnityEngine;

public class UnitySetups
{
    public static SetupPriorities Priority = SetupPriorities.EditorOnly;
    Dictionary<ISetupUnity, bool> isInit = new Dictionary<ISetupUnity, bool>();
    

    public bool RunSetup(ISetupUnity script)
    {
        if (script == null)
        {
            Debug.LogError("Some setup script is null.");
            return false;
        }
        //if (!isInit.ContainsKey(script))
        //    isInit.Add(script, false);

        if (script.UnitySetup())
        {
            //isInit[script] = true;
            return true;
        }
        return false;
    }
}