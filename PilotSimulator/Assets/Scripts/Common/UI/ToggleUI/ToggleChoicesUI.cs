using UnityEngine;

public class ToggleChoicesUI : MonoBehaviour
{
    [SerializeField] ToggleUI[] choices;
    [SerializeField] TacticChangeUI optionalTriggers;
    private void Start()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].Init();
        }
    }

    public void ShowOn(int showId)
    {
        Syncronize(false);
        choices[showId].Set(true);
        if (optionalTriggers) optionalTriggers.ChangeTacticByUI(showId);
    }

    public void ShowOff(int showId)
    {
        Syncronize(false);
        choices[showId].Set(false);
    }

    public void Syncronize(bool value)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].Set(value);
        }
    }

}
