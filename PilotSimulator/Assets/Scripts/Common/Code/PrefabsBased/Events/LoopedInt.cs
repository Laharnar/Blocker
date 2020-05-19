using UnityEngine;

[CreateAssetMenu(menuName = "Variables/LoopedInt")]
public class LoopedInt : IntVar
{
    public IntVarValue maximum;

    public void IncreaseLoopedIdBy(int i)
    {
        value = (maximum.Value+value + i) % maximum.Value;
    }

    public void IncreaseLoopedIdBy1()
    {
        value = (maximum.Value+value + 1) % maximum.Value;
    }

    public void DecreaseLoopedIdBy1()
    {
        value = (maximum.Value+value - 1) % maximum.Value;
    }
}
