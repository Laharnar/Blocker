using UnityEngine;

[System.Serializable]
public class LinkedTransformSpace {
    [SerializeField] BoolVarValue state;
    [SerializeField] bool negate = false;
    [SerializeField] SpaceVar usedSpace;

    public void UpdateUsedSpace()
    {
        bool currentValue = state.Value;
        if (negate)
            currentValue = !currentValue;

        if (currentValue)
        {
            usedSpace.value = Space.World;
        }
        else
        {
            usedSpace.value = Space.Self;
        }
    }
}
