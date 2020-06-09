using UnityEngine;

public interface IInstance
{
    IInstance Instance();
}

public interface IFileParser:IInstance
{
    string[] GetData(string line);
}

public class CSVParser : IFileParser
{
    public const int TYPE = 0;
    const char DEFAULTPARSER = ';';

    public string[] GetData(string line)
    {
        return line.Split(DEFAULTPARSER);
    }

    public IInstance Instance()
    {
        return new CSVParser();
    }
}

public interface IContent: IInstance
{
    void Assign(string[] content);
}
[System.Serializable]
public class ContentItem: IContent
{
    public const int TYPE = 0;
    [SerializeField]string[] content;

    public void Assign(string[] content)
    {
        this.content = content;
    }

    public IInstance Instance()
    {
        return new ContentItem();
    }
}
