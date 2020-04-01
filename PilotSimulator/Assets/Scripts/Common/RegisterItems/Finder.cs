using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Finder : ScriptableObject {
    public abstract List<T> SearchByAlliance<T>() where T : MonoBehaviour;

   
}
