using System.Collections;
using UnityEngine;

public class Randomizer:MonoBehaviour
{
    [SerializeField] MonoConnection randomized;

    [SerializeField] public int numOfDifferentValue = 0;
    [SerializeField] public int active = 0;
    [SerializeField] public float randomizerMin = 5;
    [SerializeField] public float randomizerMax = 5;
    float offset = 0;

    private void Start()
    {
        StartCoroutine(PickRandom());
    }

    IEnumerator PickRandom()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(randomizerMin+ offset, randomizerMax-1 + offset));

            randomized.MonoRandomized.RandomValue(active);

            active = (active + 1) % numOfDifferentValue;
        }
    }
}
