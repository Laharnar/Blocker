using System;

public interface IUpgradeMods
{
    string ModType { get; }
    float GetModSum();

    void AddObserver(StatMods monoUserMods);
    void RemoveObserver(StatMods monoUserMods);
}
