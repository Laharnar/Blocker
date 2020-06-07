using System;
using UnityEngine;

public class HealthRegenBuff : Buff
{

    [Serializable]
    public class BuffStats
    {
        public int HealthRegen;
    }
    [SerializeField] private BuffStats buff;

    protected override void OnBuffTick()
    {
        scripts.hp.RecieveDamage(-buff.HealthRegen);
    }
}
