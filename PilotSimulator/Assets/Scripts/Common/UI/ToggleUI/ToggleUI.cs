using UnityEngine;

[System.Serializable]
public class ToggleUI
{
    [SerializeField] GameObject goTarget;
    [SerializeField] bool disableWhenOff = true;
    [SerializeField] bool isOn = false;
    [SerializeField] UIOptionalPieces optional;

    public void Toggle()
    {
        Set(!isOn);
    }

    public void Set(bool value)
    {
        isOn = value;
        if(disableWhenOff)
            goTarget.gameObject.SetActive(isOn);
        else
            goTarget.gameObject.SetActive(true);
        if (isOn)
        {
            optional.SetColor(0);
        }
        else
        {
            optional.SetColor(1);
        }
    }

    internal void Init()
    {
        Set(isOn);
    }
}
