
using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MultiTypeValue))]
public class MultiValueDrawer : PropertyDrawer {

    private const float HEIGHT = 100;
    private const float INDENT = 30;
    private SerializedProperty property;
    float labelHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return HEIGHT;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        int controlId = GUIUtility.GetControlID(FocusType.Passive);

        EditorGUI.PrefixLabel(position, controlId, new GUIContent(label.text));

        // set temp values
        this.property = property;

        labelHeight = base.GetPropertyHeight(property, label);
        Rect region = new Rect(
            position.x + INDENT * 1,
            position.y + labelHeight * 1,
            position.width - INDENT - 20,
            labelHeight);

        MakeRegion(position, 1);
        TwoLinkedToggleButtons("useInt", "useFloat", "INT", "FLOAT", property, region);


        // draw INT
        position = NextLine(position, labelHeight);
        position = NextLine(position, labelHeight);
        region = MakeMiniRegion(position, 5);

        bool useDefaultInt = PrefabVsDefaultToggle("i.useDefault", property, region);
        if (useDefaultInt)
        {
            region = MakeMiniRegion(position, 10);
            Color c = GetPropertyBool("useInt", property) ? Color.green : Color.red;
            DrawDefault(region, labelHeight, "i.defaultValue", c, 0);
        }
        else
        {
            region = MakeHalfRegion(position, 2);
            DrawPrefab<IntVar>(region, labelHeight, "useInt", "i.value");
        }

        //---------------
        // draw FLOAT
        position = NextLine(position, labelHeight);
        region = MakeMiniRegion(position, 5);
        bool useDefaultFloat = PrefabVsDefaultToggle("f.useDefault", property, region);
        if (useDefaultFloat)
        {
            region = MakeMiniRegion(position, 10);
            Color c = GetPropertyBool("useFloat", property) ? Color.green : Color.red;
            DrawDefault(region, labelHeight, "f.defaultValue", c, 1);
        }
        else
        {
            region = MakeHalfRegion(position, 2);
            DrawPrefab<FloatVar>(region, labelHeight, "useFloat", "f.value");
        }
        GUI.color = Color.white;

        EditorGUI.EndProperty();
    }


    Rect NextLine(Rect position, float lineHeight)
    {
        position.y += lineHeight;
        return position;
    }
    Rect MakeHalfRegion(Rect position, int indentDepth)
    {
        Rect region = new Rect(
            position.x + (INDENT * indentDepth + position.width)/2-10,
            position.y,
            (position.width - INDENT * indentDepth - 10)/2,
            labelHeight);
        return region;
    }

    Rect MakeMiniRegion(Rect position, int indentDepth)
    {
        Rect region = new Rect(
            position.x + INDENT * indentDepth,
            position.y,
            INDENT * indentDepth-10,
            labelHeight);
        return region;
    }
    Rect MakeRegion(Rect position, int indentDepth)
    {
        Rect region = new Rect(
            position.x + INDENT * indentDepth,
            position.y,
            position.width - INDENT * indentDepth - 10,
            labelHeight);
        return region;
    }

    private void DrawDefault(Rect region, float labelHeight, string propName, Color c, int type)
    {
        GUI.color = c;
        var valueProperty = property.FindPropertyRelative(propName);

        if (type == 0)
        {
            valueProperty.intValue = EditorGUI.IntField(region, valueProperty.intValue);
        }
        else if (type == 1)
        {
            valueProperty.floatValue = EditorGUI.FloatField(region, valueProperty.floatValue);
        }
        else
        {
            Debug.Log("error, invalid type, use 0:int 1:float instead of: " + type);
        }
        GUI.color = Color.white;
    }

    private bool PrefabVsDefaultToggle(string name, SerializedProperty property, Rect region)
    {

        bool intUseDefault = GetPropertyBool(name, property);
        GUI.color = intUseDefault ? Color.blue: Color.yellow;

        bool curValue = GetPropertyBool(name, property);
        curValue = GUI.Toggle(region, curValue, curValue ? "Default" : "Prefab");
        SetProperty(name, property, curValue);
        GUI.color = Color.white;
        return curValue;
    }

    private void DrawPrefab<T>(Rect region, float labelHeight, params string[] args)
    {
        bool useInt = GetPropertyBool(args[0], property);
        if (useInt) GUI.color = Color.green;
        else GUI.color = Color.red;
        ShowObject(args[1], property, region, typeof(T));
        GUI.color = Color.white;
    }
    
    private void ShowObject(string name,SerializedProperty property, Rect region, Type type)
    {
        EditorGUI.BeginChangeCheck();
        var valueProperty = property.FindPropertyRelative(name);
        var newValue = EditorGUI.ObjectField(region, "", valueProperty.objectReferenceValue, type, false);
        if (EditorGUI.EndChangeCheck())
        {
            valueProperty.objectReferenceValue = newValue;
        }
    }

    void TwoLinkedToggleButtons(string name1, string name2, string content1, string content2, SerializedProperty property, Rect region)
    {
        EditorGUI.BeginChangeCheck();
        Rect regionPlus1 = region;
        Rect regionPlus2 = region;
        regionPlus1.x += INDENT;
        regionPlus1.width = regionPlus1.width / 2 - 20;
        regionPlus2.x += INDENT + regionPlus1.width;
        regionPlus2.width = regionPlus2.width / 2 - 20;

        DrawLinkedToggle(name1, name2, content1, property, regionPlus1);
        DrawLinkedToggle(name2, name1, content2, property, regionPlus2);
    }

    private void DrawLinkedToggle(string name1, string name2, string content, SerializedProperty property, Rect regionPlus1)
    {
        bool value1 = GetPropertyBool(name1, property);
        GUI.color = value1 ? Color.green : Color.red;
        bool on1 = (GUI.Toggle(regionPlus1, value1, content, "Button"));
        if (on1)
        {
            SetProperty(name1, property, true);
            SetProperty(name2, property, false);
        }
        GUI.color = Color.white;
    }
    
    private bool GetPropertyBool(string name1, SerializedProperty property)
    {
        var valueProperty = property.FindPropertyRelative(name1);
        return valueProperty.boolValue;
    }
    void SetProperty(string name1, SerializedProperty property, bool newValue)
    {
        var valueProperty = property.FindPropertyRelative(name1);
        valueProperty.boolValue = newValue;
    }

}
