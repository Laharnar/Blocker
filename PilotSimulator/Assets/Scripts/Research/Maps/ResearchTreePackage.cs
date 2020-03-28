using UnityEngine;

/// <summary>
/// For unlocking items in tree.
/// </summary>
[CreateAssetMenu]
public class ResearchTreePackage:ScriptableObject {
    // full research tree
    public ResearchTreeItem[] researchOptions;

    public bool Buy(int id, int researchPoints)
    {
        if (id < researchOptions.Length)
        {
            if (researchPoints >= researchOptions[id].cost.value)
            {
                researchOptions[id].unlocked.value = true;
                Debug.Log("item bought " + id + " "+ researchOptions[id].cost.value+" "+ researchPoints);
                return true;
            }
        }
        Debug.Log("item not bought " + id + " "+ researchOptions[id].cost.value + " " + researchPoints);
        return false;
    }

    public ResearchTreeItem GetItemByTag(ResearchItemTag tag)
    {
        for (int i = 0; i < researchOptions.Length; i++)
        {
            if (researchOptions[i].tag == tag)
            {
                return researchOptions[i];
            }
        }
        return null;
    }
}
