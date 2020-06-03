using UnityEngine;

public static class SingletonFactory
{
    public static void KeepSingletonInScene<T>(ref T existing) where T : MonoBehaviour
    {
        if (existing == null)
        {
            GameObject go = new GameObject();
            existing = go.AddComponent<T>();
        }
    }
}
