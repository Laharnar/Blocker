using TMPro;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    [SerializeField] MonoConnection expValue;
    [SerializeField] TMP_Text text;

    private void Update()
    {
        if (expValue.MonoUI.IsChanged)
        {
            text.text = expValue.MonoUI.GetContent();
        }
    }
}
