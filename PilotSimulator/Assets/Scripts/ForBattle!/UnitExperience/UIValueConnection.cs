using UnityEngine;

[System.Serializable]
public class UIValueConnection
{
    [SerializeField] MonoBehaviour mono;
    public IUIValue Mono => (IUIValue)mono;
}
