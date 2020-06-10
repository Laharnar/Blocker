using System.Collections.Generic;
using UnityEngine;

public class UIGroupedContext : MonoBehaviour
{
    [SerializeField] List<MonoBehaviour> logged = new List<MonoBehaviour>();
    List<IUIContextItem> items = new List<IUIContextItem>();

    internal void Register(IUIContextItem uiItem)
    {
        items.Add(uiItem);
        logged.Add((MonoBehaviour)uiItem);
    }

    internal void Unegister(IUIContextItem uiItem)
    {
        items.Remove(uiItem);
        logged.Remove((MonoBehaviour)uiItem);
    }

    public void ResetContextUI()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].ResetUI();
        }
    }
}

