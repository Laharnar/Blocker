using UnityEngine;

public class ResearchUICost : MonoBehaviour {

    public TMPro.TMP_Text text;
    [SerializeField] string prefix = "";
    [SerializeField] string suffix = "VSP";
    [SerializeField] IntVarValue val;

    private void Start()
    {
        DisplayCostWithSuffixAndPrefix();
    }

    public void Init(IntVar customVal)
    {
        Debug.Log("This is used [*2]. if not, remove it.");
        val.SetIntPrefab(customVal);
    }

    [ContextMenu("Update cost text")]
    public void DisplayCostWithSuffixAndPrefix()
    {
        text.text = "";
        if (prefix != "")
        {
            text.text += prefix;
        }
        text.text += val.Value.ToString();
        if (suffix != "")
        {
            text.text += suffix;
        }
    }
}
