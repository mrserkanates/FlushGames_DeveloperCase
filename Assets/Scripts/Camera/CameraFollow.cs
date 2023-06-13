using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera cameraToFollow;
    public float offsetX, offsetY, offsetZ; // camera offsets

    private void LateUpdate()
    {
        cameraToFollow.transform.position = transform.position +
            new Vector3(offsetX, offsetY, offsetZ);
    }
}
