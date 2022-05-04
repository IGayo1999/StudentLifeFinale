using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used for the Camera that is used to follow the player around in a fixed point of view.
public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 cameraOffset;
    public float smoothFactor = 0.5f;
    public bool lookAtTarget = false;

    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        if (lookAtTarget)
        {
            transform.LookAt(targetObject);
        }
    }

}