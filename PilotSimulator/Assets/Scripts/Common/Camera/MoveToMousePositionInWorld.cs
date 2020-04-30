using UnityEngine;

public class MoveToMousePositionInWorld : MonoBehaviour
{
    public VectorController limit;
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = pos - (Vector2)transform.position;
        transform.Translate(limit.LimitDir(dir));
    }
}
