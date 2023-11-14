using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private float cameraSpeed = 0.2f;
    private Vector3 cameraOffset = new Vector3 (4.5f, 0.1f, -1);
    private Vector3 cameraNextPosition;

    void LateUpdate()
    {
        cameraNextPosition = player.transform.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, cameraNextPosition, cameraSpeed);
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 24)
        {
            transform.position = new Vector3(24, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -1.5f)
        {
            transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
        }
        if (transform.position.y > 1.5f)
        {
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        }
    }
}
