using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
using System.Linq;

public static class CSVContent
{
    public interface ILine
    {
        void SetLine(string[] fields);
    }
    public interface ILines<T> where T:ILine
    {
        List<T> Lines { get; }

        void Add(T line);
    }

    public class TextLines<T> : ILines<T> where T : ILine
    {
        public List<T> Lines { get; }

        public void Add(T line)
        {
            Lines.Add(line);
        }
    }

    class TextLine : ILine
    {
        string[] line;

        public TextLine(string[] fields)
        {
            SetLine(fields);
        }

        public void SetLine(string[] fields)
        {
            line = fields;
        }
    }

    public static ILines<T> ReadCsv<T>(string path) where T: ILine
    {
        var defPath = @"C:\Person.csv"; // Habeeb, "Dubai Media City, Dubai"
        using (TextFieldParser csvParser = new TextFieldParser(path))
        {
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;

            // Skip the row with the column names
            csvParser.ReadLine();

            ILines<T> lines = new TextLines<T>();

            while (!csvParser.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
                string[] fields = csvParser.ReadFields();
                lines.Add((T)TextFactory.CreateTextLine(fields));
            }

            Debug.Log("Reading csv with"+lines.Lines.Count);

            return lines;
        }
    }
    static class TextFactory
    {
        public static ILine CreateTextLine(string[] fields)
        {
            return new TextLine(fields);
        }
    }
}
