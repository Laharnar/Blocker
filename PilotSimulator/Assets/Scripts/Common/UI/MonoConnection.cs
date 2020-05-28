using UnityEngine;

[System.Serializable]
public class MonoConnection
{
    [SerializeField] MonoBehaviour mono;
    public IUIValue MonoUI => (IUIValue)mono;
    public ITactic MonoTactic => (ITactic)mono;
}
