using System;
using UnityEngine;

[System.Serializable]
public class MonoConnection
{
    [SerializeField] MonoBehaviour mono;
    public IUIValue MonoUI => (IUIValue)mono;
    public ITactic MonoTactic => (ITactic) mono;
    public IWeaponMaker MonoWeaponMaker => (IWeaponMaker) mono;
    public IWeaponChanger MonoWeaponChanger => (IWeaponChanger) mono;
    public IRandomizable MonoRandomized => (IRandomizable) mono;
    public IStatGetter StatGetter => (IStatGetter)mono;

    public OnDeathEvents onDeathEvents => mono as OnDeathEvents;

    internal void Setup(MonoBehaviour speedMod)
    {
        mono = speedMod;
    }
}
