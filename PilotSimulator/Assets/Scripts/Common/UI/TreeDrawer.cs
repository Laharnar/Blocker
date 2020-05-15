using System;
using UnityEngine;
public class TreeDrawer: MonoBehaviour
{
    [SerializeField] TreeData activeTree;


    public void SetActive(TreeData data)
    {
        activeTree = data;
        Redraw();
    }

    protected virtual void Redraw()
    {
        Debug.LogError("TREE DRAWER NOT IMPLEMENTED.");
    }
}
