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

    public Timer spawnTimer;

    public TempItems prebuiltVisuals;

    private int placementMode = 0;
    private int mode = 0;
    private int buildId = 0;

    private Camera mainCam;
    private Transform temporarySpawned;
    
    private Ray buildRay;
    private BuildMode activeBuildMode;
    private bool hoveringOverBuildLocation;
    private bool mousePressed;
    private bool delayPassed;

    private const int READY = 0;
    private const int PLACE_CONTINOUSLY = 1;
    private const int PLACE_CONTINOUSLY_PLACED_ONE = 4;
    private const int PLACE_ONCE = 3;
    private const int CLEANUP = 2;

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        mainCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        // in build mode
        if (mode > READY)
        {
            InBuildMode();
        }
    }

    private void InBuildMode()
    {
        temporarySpawned.position = FindBuildLocationUnderMouse();

        HandleBuildConditions();

        if (BuildRequestValid())
        {
            if (hoveringOverBuildLocation)
            {
                Construct(temporarySpawned.position);
                SetBuildModeAfterBuilding(); 
                SetTimerForNextAllowedBuild();
            }
            else
            {
                Debug.Log("state: Invalid location to build, aborting.");
                //Construct(r.origin + r.direction);
            }
        }

        // allow multiple build modes
        if (Input.GetKey(KeyCode.Space))
        {
            placementMode = 1;
            prebuiltVisuals.SphericalPlacement(referencePoint.position);
        }
        else
        {
            placementMode = 0;
            prebuiltVisuals.DirectionalPlacement(referencePoint.position);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mode = CLEANUP;
        }

        if (Input.GetMouseButtonUp(0) && mode == PLACE_CONTINOUSLY_PLACED_ONE)
        {
            mode = CLEANUP;
        }

        // cancel building only in buils mode
        if (mode == CLEANUP)
        {
            EndBuilding();
        }
    }

    private void SetBuildModeAfterBuilding()
    {
        if (mode == PLACE_CONTINOUSLY || mode == PLACE_CONTINOUSLY_PLACED_ONE)
        {
            mode = PLACE_CONTINOUSLY_PLACED_ONE;
        }
        else if (mode == PLACE_ONCE)
        {
            mode = CLEANUP;
        }
        else
        {
            throw new NotImplementedException("Unknown mode " + mode);
        }
    }

    private Vector3 FindBuildLocationUnderMouse()
    {
        RaycastHit hit;
        buildRay = mainCam.ScreenPointToRay(Input.mousePosition);
        if (IsOverBuildLocation(out hit))
        {
            hoveringOverBuildLocation = true;
            return SnapBuildLocation(hit.point);
        }
        else
        {
            return new Vector3(10000, 0);
        }
    }

    private static Vector3 SnapBuildLocation(Vector3 placeOn)
    {
        placeOn.x = (int)placeOn.x;
        placeOn.y = (int)placeOn.y;
        placeOn.z = (int)placeOn.z;
        return placeOn;
    }

    private bool BuildRequestValid()
    {
        if (mode == PLACE_CONTINOUSLY || mode == PLACE_CONTINOUSLY_PLACED_ONE)
        {
            if(mousePressed && spawnTimer.Ready())
                return true;
        }
        else if (mode == PLACE_ONCE)
        {
            if(mousePressed)
                return true;
        }
        else if (mode == CLEANUP)
        {
            return false;
        }
        else
        {
            throw new NotImplementedException("Unknown mode " + mode);
        }
        return false;
    }

    private bool IsOverBuildLocation(out RaycastHit hit)
    {
        return Physics.Raycast(buildRay, out hit, Mathf.Infinity, buildLayerMask, QueryTriggerInteraction.Collide);
    }

    private void HandleBuildConditions()
    {
        mousePressed = Input.GetKey(KeyCode.Mouse0);
        delayPassed = spawnTimer.Ready();
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
        Transform t= Instantiate(buildPrefabs[buildId], position, new Quaternion());
    }
    void SetTimerForNextAllowedBuild()
    {
        spawnTimer.Trigger();
    }

    // called from ui.
    public void SetBuildMode(int buildMode)
    {
        activeBuildMode = (BuildMode)buildMode;
    }
    public void BeginBuilding1(BuildMode asd)
    {
    }
        // called from ui.
    public void BeginBuilding()
    {
        if (mode == READY)
        {
            if (activeBuildMode == BuildMode.Blocks)
            {
                mode = PLACE_CONTINOUSLY;
            }
            else if(activeBuildMode == BuildMode.Building)
            {
                mode = PLACE_ONCE;
            }
            else
            {
                throw new NotImplementedException("Unknown mode " + mode);
            }
            temporarySpawned = Instantiate(preBuildPrefabs[buildId]);
        }
        else Debug.Log("Already building.");
    }

    public void EndBuilding()
    {
        mode = READY;
        if (temporarySpawned!= null)
        {
            Destroy(temporarySpawned.gameObject);
        }

        prebuiltVisuals.ClearAll();
    }
    
}
