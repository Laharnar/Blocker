using UnityEngine;

[CreateAssetMenu]
public class OnRaycastSurface : ScienceEffect {

    public RaycastPiece rcast = new RaycastPiece();

    public BoolVarValue useSetting;
    public FloatVar movement;
    public DoPrefab onRaycast;

    public void IfThenStop(Transform t)
    {
        rcast.position.Value = t.position;
        rcast.Raycast(t);
        if (rcast.anyHit.Value)
        {
            if (useSetting.Value)
                onRaycast?.DoIt();
        }
    }

    public void IfThenStop(TransformRef t)
    {
        IfThenStop(t.value);
    }

    public void IfThenStop(TransformVarValue t)
    {
        IfThenStop(t.Value);
    }

    protected override void Activate(ScienceArgs args)
    {
        IfThenStop(args.source);

        args.moveDir.y = movement.Value;
    }


    // if you want to convert it into prefab
    //[CreateAssetMenu(menuName = "UnityCommon/Raycast")]
    [System.Serializable]
    public class RaycastPiece{//Prefab : ScriptableObject {
        public Vec3VarRef position;
        public Vec3VarRef direction;
        [Header("Raycast results")]
        public BoolVarValue anyHit;
        public TransformVarValue theHit;
        public float maxDistance=10;
        public LayerMask castedLayers;

        Camera Cam { get => Camera.main; }


        public void Raycast()
        {
            anyHit.Value = Physics.Raycast(new Ray(position.Value, direction.Value));
        }

        public void Raycast(Transform t)
        {
            RaycastHit hit;
            anyHit.Value = Physics.Raycast(t.position, direction.Value, out hit, maxDistance, castedLayers);
            if(anyHit.Value)
                theHit.Value = hit.transform;
        }
    }
}
