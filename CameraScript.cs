using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 YMinMax = new Vector2(4, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float Xcordinate;
    float Ycordinate;

    private void Start()
    {
        //lockt de cursor
        if (lockCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LateUpdate()
    {
        Xcordinate += Input.GetAxis("Mouse X") * mouseSensitivity;
        Ycordinate -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        //zorgt ervoor dat de Camera niet onder de map kan en niet overzichzelf kan draaien 
        Ycordinate = Mathf.Clamp(Ycordinate, YMinMax.x, YMinMax.y);

        //maakt de camera smoother
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(Ycordinate, Xcordinate), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        //distance van camera
        transform.position = target.position - transform.forward * dstFromTarget;
    }
}
