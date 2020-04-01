using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialStorage : GroupOfRegistered {

    [SerializeField] IntVarValue materials;
    [SerializeField] IntVarValue generatedEnergy;
    [SerializeField] Timer repeatRate;
    GroupOfRegistered detectors1;

    private void Start()
    {
        detectors1 = this;
        StartCoroutine(Absorb());
    }

    IEnumerator Absorb()
    {
        yield return null;
        // todo: find cubes in range
        while (true)
        {
            for (int i = 0; i < detectors1.Registred.Count; i++)
            {
                OnTriggerFindGroup materialRegister = detectors1.GetScript<OnTriggerFindGroup>(i);
                if (materialRegister)
                {
                    Register[] t = materialRegister.GetAllCostly();
                    AbsorbEnergy(t);
                }
                else
                {
                    Debug.LogError("Detector doesn't have MaterialAbsorber.");
                }
            }

            repeatRate.Trigger();
            yield return repeatRate.WaitReady();
        }
    }

    private void AbsorbEnergy(Register[] t)
    {
        generatedEnergy.Value += t.Length;
    }
}
