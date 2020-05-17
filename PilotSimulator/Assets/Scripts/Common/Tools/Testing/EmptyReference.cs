using System;
using UnityEngine;

public class EmptyReference
{
    public static T Initializer<T>(MonoBehaviour source) where T : Component
    {
        Debug.Log("[Automatic setup] Creating temporary object: TempAutoReference linked to " + typeof(T), source);
        T comp = new GameObject("[delete any time]TempAutoReference"+typeof(T)).AddComponent<T>();
        return comp;
    }

    internal static bool IsNotEmpty(PlaceHolderUI targetUI)
    {
        return targetUI != null && !targetUI.name.StartsWith("[delete");
    }

    public static void Initializer<T>(MonoBehaviour source, ref T target) where T : Component
    {
        if (target == null)
        {
            Debug.Log("[Automatic setup] Creating temporary object: TempAutoReference linked to " + typeof(T), source);
            T comp = new GameObject("[delete any time]TempAutoReference" + typeof(T)).AddComponent<T>();
            target = comp;
        }
    }
    public static void DestroyTemporaryReference(MonoBehaviour obj)
    {
        GameObject.Destroy(obj.gameObject);
    }
}
