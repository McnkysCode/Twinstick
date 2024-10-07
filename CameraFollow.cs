using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // Reference to the player's transform
    [SerializeField] private float smoothSpeed = 0.125f;  // Speed of camera movement
    [SerializeField] private float rotationDamping = 10f;  // Damping factor for camera rotation

    private Vector3 offset;  // Offset between the camera and player

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationDamping * Time.deltaTime);
    }
}