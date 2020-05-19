using System.Collections.Generic;
using UnityEngine;

public class LinkerRoot:MonoBehaviour
{
    public List<Linker> linkers = new List<Linker>();

    public void Setup(Linked linked)
    {
        for (int i = 0; i < linkers.Count; i++)
        {
            linkers[i].Connect(linked);
        }
    }
}