using System.Linq;
using UnityEngine;

[System.Serializable]
public class Bonus : IModData
{
    [SerializeField] public string type;
    [SerializeField] public float value;

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

    public override string ToString()
    {
        return string.Format("{0}{1}", type, value);
    }
}
