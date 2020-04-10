
using System;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TwoValueOperation))]
public class TwoValueOperationDrawer:Editor {

    private const float HEIGHT = 100;
    private const float INDENT = 30;
    float labelHeight;

    public override void OnInspectorGUI()
    {
        TwoValueOperation t = (TwoValueOperation)target;
        base.OnInspectorGUI();

        GUI.color = Color.red;
        StringBuilder formula = new StringBuilder();
        EditorGUILayout.LabelField(t.name);

        try
        {
            EditorGUILayout.LabelField("A = " + t.a.Value+" and/or "+ AddBracesIfAny(t.a.GetNameOrDefault("")));
            EditorGUILayout.LabelField("B = " + t.b.Value + " and/or " + AddBracesIfAny(t.b.GetNameOrDefault("")));
            EditorGUILayout.LabelField("Result = " + t.result.Value + " and/or " + AddBracesIfAny(t.result.GetNameOrDefault("")));

            formula.Clear();
            formula.Append(t.a.GetNameOrDefault("A"));
            formula.Append("    ");
            formula.Append(t.OpAsString());
            formula.Append("    ");
            formula.Append(t.b.GetNameOrDefault("B"));
            formula.Append("    =   ");
            formula.Append(t.result.GetNameOrDefault("Result"));

            EditorGUILayout.LabelField(formula.ToString());

            formula.Clear();
            formula.Append(t.a.Type);
            formula.Append("    ");
            formula.Append(t.OpAsString());
            formula.Append("    ");
            formula.Append(t.b.Type);
            formula.Append("    =   ");
            formula.Append(t.result.Type);

            EditorGUILayout.LabelField(formula.ToString());

            formula.Clear();
            formula.Append(t.a.Value);
            formula.Append(AddBracesIfAny(t.a.GetNameOrDefault("")));
            formula.Append("    ");
            formula.Append(t.OpAsString());
            formula.Append("    ");
            formula.Append(t.b.Value);
            formula.Append(AddBracesIfAny(t.b.GetNameOrDefault("")));
            formula.Append("    =   ");
            formula.Append(t.result.Value);
            formula.Append(AddBracesIfAny(t.result.GetNameOrDefault("")));

            EditorGUILayout.LabelField(formula.ToString());
        }
        catch (NullReferenceException)
        {
            EditorGUILayout.LabelField("One of values is null. Can't execute formula. Won't pass at runtime.");
            Debug.LogError("One of values is null. Can't execute formula. Won't pass at runtime. "+ t.name, t);
        }

    }

    string AddBracesIfAny(string s)
    {
        if(s!= "")
        {
            return "(" + s + ")";
        }
        return s;
    }


}
