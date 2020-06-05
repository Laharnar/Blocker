using UnityEngine;

[System.Serializable]
public class Bonus : IModData
{
    [SerializeField] public string type;
    [SerializeField] public int value;

    public string ModType { get => type; }
    public float ModValue { get => value; }

    public IModData Copy()
    {
        return new Bonus()
        {
            type = this.type,
            value = this.value
        };
    }
}
