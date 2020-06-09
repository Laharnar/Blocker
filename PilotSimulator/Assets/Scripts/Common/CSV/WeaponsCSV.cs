using System.Collections.Generic;
using UnityEngine;

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
