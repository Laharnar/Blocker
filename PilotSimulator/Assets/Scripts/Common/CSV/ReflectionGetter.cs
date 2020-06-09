using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public static class ReflectionGetter
{
    public  static int GetStaticType(Type type)
    {
        FieldInfo[] constants = ReflectionGetter.GetConstants(type);
        if(constants.Length == 0)
        {
            Debug.Log("No constant identifiers in type "+type);
        }
        FieldInfo assumedType = constants[0];
        if (assumedType.Name == "TYPE")
        {
            int typeValue = (int)assumedType.GetValue(type);
            return typeValue;
        }
        Debug.LogError("Failed to find match");
        return -1;
    }
    public static FieldInfo[] GetConstants(System.Type type)
    {
        ArrayList constants = new ArrayList();

        FieldInfo[] fieldInfos = type.GetFields(
            // Gets all public and static fields

            BindingFlags.Public | BindingFlags.Static |
            // This tells it to get the fields from all base types as well

            BindingFlags.FlattenHierarchy);

        // Go through the list and only pick out the constants
        foreach (FieldInfo fi in fieldInfos)
            // IsLiteral determines if its value is written at 
            //   compile time and not changeable
            // IsInitOnly determines if the field can be set 
            //   in the body of the constructor
            // for C# a field which is readonly keyword would have both true 
            //   but a const field would have only IsLiteral equal to true
            if (fi.IsLiteral && !fi.IsInitOnly)
                constants.Add(fi);

        // Return an array of FieldInfos
        return (FieldInfo[])constants.ToArray(typeof(FieldInfo));
    }
}