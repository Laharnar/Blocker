using System.Collections.Generic;
using UnityEngine;

public class CommanderOfGroups:MonoBehaviour
{
    public List<TacticGroup> groups = new List<TacticGroup>();

    public void SkillDamageGroups(int value)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            groups[i].SkillDamageGroup(value);
        }
    }
}