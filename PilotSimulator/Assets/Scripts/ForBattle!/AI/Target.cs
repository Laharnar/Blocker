using UnityEngine;

[System.Serializable]
public class Target
{
    public Vector3 target;
    public int alliance = 0;

    public Target(Vector3 target, int alliance)
    {
        this.target = target;
        this.alliance = alliance;
    }
}
