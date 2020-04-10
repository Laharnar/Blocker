using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IncomeStorage : GroupOfRegistered {

    [SerializeField] IntVarValue materials;
    [SerializeField] Timer repeatRate;
    GroupOfRegistered detectors1;
    public FloatVarRef multiplierForOtherIncome;
    public FloatVarRef multiplierForConstantIncome;

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
            if (multiplierForConstantIncome.Value == 0)
            {
                Debug.LogError("Income multiplier is 0.");
            }
            if (multiplierForOtherIncome.Value == 0)
            {
                Debug.LogError("Income multiplier is 0.");
            }
            for (int i = 0; i < detectors1.Registred.Count; i++)
            {
                MonoBehaviour materialRegister = detectors1.GetScript<MonoBehaviour>(i);
                AbsorbEnergyFromDifferentTypes(materialRegister);
            }

            repeatRate.Trigger();

            yield return repeatRate.WaitReady();
        }
    }

    private void AbsorbEnergyFromDifferentTypes(MonoBehaviour materialRegister)
    {
        if (materialRegister as GroupOfRegistered)
        {
            Register[] t = ((GroupOfRegistered)materialRegister).Registred.ToArray();
            AbsorbEnergy(t);
        }
        else if (materialRegister as ConstantIncome)
        {
            ConstantIncome t = ((ConstantIncome)materialRegister);
            AbsorbEnergy(t);
        }
        else
        {
            Debug.LogError("Detector doesn't have known MonoBehaviour type?");
        }
    }

    private void AbsorbEnergy(ConstantIncome t)
    {
        materials.Value += (int)(t.incomePerSecond.Value * multiplierForConstantIncome.Value);
    }

    private void AbsorbEnergy(Register[] t)
    {
        materials.Value += (int)(t.Length * multiplierForOtherIncome.Value);
    }
}
