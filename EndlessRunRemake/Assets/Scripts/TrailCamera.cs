using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCamera : MonoBehaviour
{
    public Transform target;
    public float trailDistance = 5.0f;
    public float heightOffset = 4.0f;
    public float cameraDelay = 0.01f;


    void Update()
    {
        Vector3 followPos = target.position - trailDistance * target.forward;
        followPos.y += heightOffset;
        transform.position = Vector3.Lerp(followPos, transform.position, 0.99f);
        transform.LookAt(target.transform);
    }


}
