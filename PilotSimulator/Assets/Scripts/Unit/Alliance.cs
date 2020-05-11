using System;
using UnityEngine;
using UnityEngine.Events;

// replace it with prefab veriation, intvarvalue
public class Alliance:MonoBehaviour, ITestable {
    public int thisAlliance;
    public int allianceId;
    public int AllianceId => allianceId;

    public void TestInitialState()
    {
        allianceId = thisAlliance;
    }
}
