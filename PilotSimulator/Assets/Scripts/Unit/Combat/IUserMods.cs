using System;

public interface IUserMods
{
    string ModType { get; }
    float GetModSum();

    Action<float> OnModGetsNewValue { get; set; }
}
