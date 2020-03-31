using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Timer {

    public float rate = 1;
    float nextReady = 0;

    public bool Ready()
    {
        return Time.time > nextReady;
    }

    public void Trigger()
    {
        nextReady = Time.time + rate;
    }

    public IEnumerator WaitReady()
    {
        yield return new WaitForSeconds(rate);
    }

}
