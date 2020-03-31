using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaterialAbsorber:MonoBehaviour {
    List<Absorbable> absorbables = new List<Absorbable>();
    public Absorbable[] Nearby { get => absorbables.ToArray(); }
    public UnityEvent OnStart;
    public UnityEvent OnFindNew;
    public UnityEvent OnLostOne;
    public UnityEvent OnLostAll;

    public bool autoRegister = false;
    public Alliance alliance;

    private void Start()
    {
        OnStart?.Invoke();

        if (autoRegister)
            RegisterToOneAllyAbsorber();
    }

    private void RegisterToOneAllyAbsorber()
    {
        Absorber[] absorbers = GameObject.FindObjectsOfType<Absorber>();
        for (int i = 0; i < absorbers.Length; i++)
        {
            if (alliance.thisAlliance == absorbers[i].alliance.thisAlliance)
            {
                absorbers[i].AddAbsorber(this);
                break;
            }
        }
    }

    public void FindNew(Absorbable detectable)
    {
        absorbables.Add(detectable);
        OnFindNew.Invoke();
    }

    public void Lost(Absorbable detectable)
    {
        absorbables.Remove(detectable);
        OnLostOne.Invoke();

        if (absorbables.Count == 0)
            OnLostAll.Invoke();
    }

    protected void OnTriggerEnter(Collider other)
    {
        Absorbable o = other.gameObject.GetComponent<Absorbable>();
        if (o)
        {
            FindNew(o);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Absorbable o = other.gameObject.GetComponent<Absorbable>();
        if (o)
        {
            Lost(o);
        }
    }
}
