using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialStorage : GroupOfRegistered {

    [SerializeField] IntVarValue materials;
    [SerializeField] IntVarValue generatedEnergy;
    [SerializeField] Timer repeatRate;
    GroupOfRegistered detectors1;
    public FloatVarRef multiplierForBasicIncome;
    public FloatVarRef multiplierForAbsorbers;

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
                MonoBehaviour materialRegister = detectors1.GetScript<MonoBehaviour>(i);
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

            repeatRate.Trigger();
            yield return repeatRate.WaitReady();
        }
    }

    private void AbsorbEnergy(ConstantIncome t)
    {
        generatedEnergy.Value += (int)(t.incomePerSecond.Value * multiplierForBasicIncome.Value);
    }

    private void AbsorbEnergy(Register[] t)
    {
        generatedEnergy.Value += (int)(t.Length *multiplierForAbsorbers.Value);
    }
}
