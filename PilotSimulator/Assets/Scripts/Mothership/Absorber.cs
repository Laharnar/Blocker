using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Absorber:MonoBehaviour {
    [SerializeField] IntVarValue materials;
    [SerializeField] IntVarValue generatedEnergy;
    [SerializeField] Timer repeatRate;
    [SerializeField] List<MaterialAbsorber> detectors = new List<MaterialAbsorber>();
    public Alliance alliance;

    private void Start()
    {
        StartCoroutine(Absorb());
    }

    IEnumerator Absorb()
    {
        // todo: find cubes in range
        while (true)
        {
            for (int i = 0; i < detectors.Count; i++)
            {
                Absorbable[] t = detectors[i].Nearby;
                AbsorbEnergy(t);
            }

            repeatRate.Trigger();
            yield return repeatRate.WaitReady();
        }
    }

    private void AbsorbEnergy(Absorbable[] t)
    {
        generatedEnergy.Value += t.Length;
    }

    public void AddAbsorber(Transform absorber)
    {
        MaterialAbsorber item = absorber.GetComponent<MaterialAbsorber>();
        if(item!= null)
            detectors.Add(item);
    }

    public void AddAbsorber(MaterialAbsorber absorber)
    {
        detectors.Add(absorber);
    }

    public void RemoveAbsorber(MaterialAbsorber absorber)
    {
        detectors.Remove(absorber);
    }
}
