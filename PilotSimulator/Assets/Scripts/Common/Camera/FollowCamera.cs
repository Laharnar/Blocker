using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    [Range(0.0f, 1.0f)]
    public float followStrength = 0.8f;
    public VectorController controller;
    public float speed = 30;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Debug.Log("Followed target is missing.");
            return;
        }
        Vector3 v = Vector3.Lerp(transform.position, target.position, followStrength);
        Vector3 dir = target.position - transform.position;
        dir = controller.LimitDir(dir);
        transform.Translate(dir * Time.deltaTime * speed);
    }
}
