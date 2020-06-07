using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BonusList : MonoBehaviour, IPrefabLoadable
{
    [SerializeField] List<Bonus> bonuses = new List<Bonus>();

    public int Count => bonuses.Count;

    public Bonus GetBonus(int id)
    {
        return bonuses[id];
    }

    public string ToUI()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bonuses.Count; i++)
        {
            builder.Append(string.Format("{0}  :  {1}\n", 
                bonuses[i].type, bonuses[i].value));
        }
        return builder.ToString();
    }
}
