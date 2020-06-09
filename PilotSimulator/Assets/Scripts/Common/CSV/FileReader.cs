using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class FileReader
{
    static TypeFactory contentTemplates = new TypeFactory(
        new IInstance[] { new ContentItem() } );


    static TypeFactory parsers = new TypeFactory(
        new IInstance[] { new CSVParser() });
    public static List<CT> ReadFileTrimStart<PT, CT>(string path, int trimStartLines)
        where PT : IFileParser where CT : IContent
    {
        List<CT> content = ReadFile<PT, CT>(path);

        content.RemoveRange(0, trimStartLines);
        return content;
    }
    public static List<CT> ReadFile<PT, CT>(string path) 
        where PT : IFileParser where CT : IContent
    {
        var defPath = @"C:\Person.csv";
        //Open the stream and read it back.  
        int fileFormat = 0;
        IContent contentTemplate =(IContent) contentTemplates.GetByType(fileFormat);
        IFileParser parser =(IFileParser) parsers.GetByType(fileFormat);
        
        List<CT> content = new List<CT>();

        if (File.Exists(path)) {
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                IContent contentItem = (IContent) contentTemplate.Instance();
                contentItem.Assign(parser.GetData(lines[i]));
                content.Add((CT)contentItem);
            }
        }
        else
        {
            Debug.LogError($"File doesn't exist. {path}");
        }
        return content;
    }
}
