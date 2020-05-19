using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some sort of manager finder exposed as prefab.
/// </summary>
[CreateAssetMenu(menuName = "Short commands/FindAlliedMaterialStorage")]
public class FindAlliedScript: Finder {

    public IntVarValue alliance;

    public override List<T> SearchByAlliance<T>()
    {
        MonoBehaviour[] scripts = GameObject.FindObjectsOfType<T>();
        List<T> allyScripts = new List<T>();
        for (int i = 0; i < scripts.Length; i++)
        {
            if (HasComponentAllianceAndMatch(scripts[i]))
            {
                allyScripts.Add((T)scripts[i]);
            }
        }

        return allyScripts;
    }

    private bool HasComponentAllianceAndMatch(MonoBehaviour item)
    {
        AllianceRef a = item.GetComponent<AllianceRef>();
        if (a != null)
        {
            return a.alliance.thisAlliance == alliance.Value;
        }
        else
        {
            throw new System.NullReferenceException("AllianceRef script doesn't exist on object. name:" + item.name);
        }
    }
}
