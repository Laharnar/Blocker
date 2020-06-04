using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusList : MonoBehaviour
{
    [SerializeField] List<Bonus> bonuses = new List<Bonus>();

    public int Count => bonuses.Count;

    public Bonus GetBonus(int id)
    {
        return bonuses[id];
    }
}
