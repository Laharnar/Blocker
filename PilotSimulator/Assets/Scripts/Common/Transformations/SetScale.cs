using UnityEngine;
public class MyClass
{

}
public class SetScale : MonoBehaviour
{
    [Header("Random")]
    public BoolVarValue setRandom;
    public RectVarValue randomAreaAround;

    [Header("Controlled")]
    public BoolVarValue useControlled;
    public IntVarValue active;
    public Vector3[] relativeScales;
    public TransformVarValue target;

    public MultiVarToVector3Setter setter;
    public bool useInUpdate = true;

    void Awake()
    {
        target.Value = transform;
    }

    private void Update()
    {
        if (useInUpdate)
        {
            SetToSelectedScale();
        }
    }

    public void Next()
    {
        active.Value = (active.Value + 1) % relativeScales.Length;
    }

    public void SetToSelectedScale()
    {
        if (setRandom.Value)
        {
            target.Value.localScale = NewRandom(randomAreaAround.Value);
        }
        else if(useControlled.Value)
        {
            target.Value.localScale = relativeScales[active.Value];
        }
        else
        {
            setter.Activate();
            target.Value.localScale = setter.VecValue;
        }
    }

    Vector2 NewRandom(Rect rect)
    {
        return new Vector2(
            randomAreaAround.Value.x + Random.value * randomAreaAround.Value.width,
            randomAreaAround.Value.y + Random.value * randomAreaAround.Value.height);
    }
}