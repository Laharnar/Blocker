using TMPro;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    [SerializeField] UIValueConnection expValue;
    [SerializeField] TMP_Text text;

    private void Update()
    {
        if (expValue.Mono.IsChanged)
        {
            text.text = expValue.Mono.GetContent();
        }
    }
}
