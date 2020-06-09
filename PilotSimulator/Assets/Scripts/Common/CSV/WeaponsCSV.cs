using System.Collections.Generic;
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

public class WeaponsCSV:MonoBehaviour
{
    public PathGet paths;
    public List<ContentItem> items;

    private void Start()
    {
        RunFileRead();
    }

    [ContextMenu("File read")]
    void RunFileRead()
    {
        items = FileReader.ReadFileTrimStart<CSVParser, ContentItem>(paths.GetPath(), 1);
    }
}
