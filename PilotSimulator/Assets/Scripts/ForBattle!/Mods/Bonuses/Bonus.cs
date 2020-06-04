using UnityEngine;

[System.Serializable]
public class Bonus : IModData
{
    [SerializeField] public string type;
    [SerializeField] public int value;

    public string ModType { get => type; }
    public float ModValue { get => value }
}
