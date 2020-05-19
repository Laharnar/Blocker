using System.Collections.Generic;
using UnityEngine;

public class TreeList:PlaceHolderView
{
    public List<TreeData> allTrees = new List<TreeData>();
    public TreeDrawer drawer;

    public override void ResponseHandler(ResponseToClick useCase)
    {
        ChangeTree(useCase);
    }

    public void ChangeTree(ResponseToClick code)
    {
        if (code.userId >= allTrees.Count) {
            Debug.Log("Changed tree.");
            return;
        }
        drawer.SetActive(allTrees[code.userId]);
    }
}
