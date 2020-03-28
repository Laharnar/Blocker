using System;
using UnityEngine.Events;

[Serializable]
public class ResearchTreeItem {
    public string name;
    public ResearchItemTag tag;
    public BoolVar unlocked;
    public IntVar cost;
    public FloatVar weight;
    public UnityEvent onBuy;
}
