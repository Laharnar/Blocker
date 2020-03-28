using System;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCluster : MonoBehaviour {

    List<ResearchResult> data = new List<ResearchResult>();
    public List<ResearchResult> AllyDeaths { get => data; }

    public Action<ResearchResult> onRecieveResearch;
    public ResearchTree tree;
    public ScienceArgsVar scienceArgs;

    public bool log;
    
    internal void RegisterNewResearch(ResearchResult results)
    {
        if (log)
        {
            Debug.Log("recieved research in cluster.", this);
        }
        data.Add(results);
        if (scienceArgs != null)
            scienceArgs.args.knownAllyDeaths = AllyDeaths;
        else Debug.Log("Science args is null.", this);

        if (tree) tree.RecieveResearchPoints(1);

        onRecieveResearch?.Invoke(results);
    }

    // Used by SendMessage system. Init.
    public void NewSpawn(Transform spawned)
    {
        spawned.GetComponent<Spawned>().SetCluster(this);
    }
}
