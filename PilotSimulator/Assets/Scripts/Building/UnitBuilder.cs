using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilder : MonoBehaviour
{

    public Transform[] preBuildPrefabs;
    public Transform[] buildPrefabs;

    public LayerMask buildLayerMask;
    public Transform referencePoint;
    public bool onlyDown;

    public ReferenceCall onBuild;
    public TempItems temp;

    private int placementMode = 0;
    private int mode = 0;
    private int buildId = 0;

    private Camera mainCam;
    private Transform temporarySpawned;

    public Timer spawnTimer;
    
    private Ray buildRay;
    private bool buildConditionCleared;


    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        mainCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (mode > 0)
        {

            RaycastHit hit;
            buildRay = mainCam.ScreenPointToRay(Input.mousePosition);
            bool hoveringOverBuildLocation = Physics.Raycast(buildRay, out hit, Mathf.Infinity, buildLayerMask, QueryTriggerInteraction.Collide);
            if (hoveringOverBuildLocation)
            {
                Vector3 placeOn = hit.point;
                placeOn.x = (int)placeOn.x;
                placeOn.y = (int)placeOn.y;
                placeOn.z = (int)placeOn.z;
                temporarySpawned.position = placeOn;
            }
            else
            {
                temporarySpawned.position = new Vector3(10000, 0);
            }
            buildConditionCleared = false;
            HandleBuildConditions();
            

            // build on valid location.
            if (buildConditionCleared)
            {
                Debug.Log("asd");
                if (hoveringOverBuildLocation)
                {
                Debug.Log("bsd");
                    Construct(hit.point);
                    mode = 2;
                    spawnTimer.Trigger();
                }
                else
                {
                    Debug.Log("Invalid location to build, aborting.");
                    //Construct(r.origin + r.direction);
                }

                if (onlyDown)
                    EndBuilding();
            }

            // allow multiple build modes
            if (Input.GetKey(KeyCode.Space))
            {
                placementMode = 1;
                temp.SphericalPlacement(referencePoint.position);
            }
            else
            {
                placementMode = 0;
                temp.DirectionalPlacement(referencePoint.position);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndBuilding();
            }

        }

        if (!onlyDown && mode == 2)
        {
            if (Input.GetMouseButtonUp(0))
                EndBuilding();
        }
    }

    private void HandleBuildConditions()
    {

        buildConditionCleared = Input.GetKey(KeyCode.Mouse0);
        buildConditionCleared &= spawnTimer.Ready();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(buildRay);
    }

    public void SetBuildId(int id)
    {
        buildId = id;
    }
    public void Construct(Vector3 position)
    {
        Debug.Log("building.");
        Transform t= Instantiate(buildPrefabs[buildId], position, new Quaternion());

        onBuild.ActivateCall(t);
    }

    // called form ui.
    public void BeginBuilding()
    {
        if (mode == 0)
        {
            mode = 1;
            temporarySpawned = Instantiate(preBuildPrefabs[buildId]);
        }
        else Debug.Log("Already building.");
    }

    public void EndBuilding()
    {
        mode = 0;
        if (temporarySpawned!= null)
        {
            Destroy(temporarySpawned.gameObject);
        }

        temp.ClearAll();
    }
}
