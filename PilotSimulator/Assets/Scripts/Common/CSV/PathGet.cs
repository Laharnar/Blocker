using UnityEngine;

[System.Serializable]
public class PathGet
{
    [SerializeField] string path;

    public string GetPath()
    {
        return path;
    }
}
