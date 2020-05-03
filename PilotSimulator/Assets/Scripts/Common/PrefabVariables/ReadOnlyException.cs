using System;

public class ReadOnlyException : Exception
{
    public ReadOnlyException(string message) : base(message)
    {
    }
}
