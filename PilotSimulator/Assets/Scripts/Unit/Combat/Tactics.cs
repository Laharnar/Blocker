using System;
using System.Collections.Generic;

[Serializable]
public class Tactics
{
    public List<ITactic> tactics = new List<ITactic>();

    public void EnableNewTactic(ITactic tactic)
    {
        tactics.Add(tactic);
    }
}
