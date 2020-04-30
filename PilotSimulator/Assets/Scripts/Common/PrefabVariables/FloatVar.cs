using System;
using UnityEngine;

[CreateAssetMenu()]
public class FloatVar : ScriptableObject {
    [SerializeField] float value1;
    public float Value { get => value1; set { this.value1 = value; } }

}
