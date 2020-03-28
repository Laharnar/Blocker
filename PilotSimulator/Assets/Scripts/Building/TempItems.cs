using UnityEngine;

public class TempItems:MonoBehaviour {
    public Transform tempSpherePrefab;
    public Transform tempDirectionsPrefab;
    public int scaling;

    private Transform tempSphereVisual;
    private Transform tempDirectionsVisual;


    public void SphericalPlacement(Vector3 pos)
    {
        if (tempSphereVisual == null)
        {
            tempSphereVisual = Instantiate(tempSpherePrefab, pos, Quaternion.identity);
        }
        else
        {
            tempSphereVisual.localScale = Vector3.one * scaling;
        }

        if (tempDirectionsVisual != null)
        {
            Destroy(tempDirectionsVisual.gameObject);
        }
    }

    public void DirectionalPlacement(Vector3 pos)
    {
        if (tempDirectionsVisual == null)
        {
            tempDirectionsVisual = Instantiate(tempDirectionsPrefab, pos, Quaternion.identity);
        }
        else
        {
            tempDirectionsVisual.localScale = new Vector3(scaling, 1, scaling);
        }

        if (tempSphereVisual != null)
        {
            Destroy(tempSphereVisual.gameObject);
        }
    }
    public void ClearAll()
    {

        if (tempDirectionsVisual != null)
        {
            Destroy(tempDirectionsVisual.gameObject);
        }
        if (tempSphereVisual != null)
        {
            Destroy(tempSphereVisual.gameObject);
        }
    }
}
