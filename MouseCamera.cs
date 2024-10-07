using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public Vector2 turn;
    [SerializeField]public float sensitivity = 10;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
}
