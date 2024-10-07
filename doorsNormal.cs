using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorsNormal : MonoBehaviour
{
    public Transform LeftDoor;
    public Transform RightDoor;
    [SerializeField] public float rightDoorLoc;
    [SerializeField] public float rightDoorReturnLoc;
    [SerializeField] public float leftDoorLoc;
    [SerializeField] public float leftDoorReturnLoc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LeftDoor.transform.Translate(leftDoorLoc, 0, 0);
            RightDoor.transform.Translate(rightDoorLoc, 0, 0);
        }
        else
        {
            LeftDoor.transform.Translate(0, 0, 0);
            RightDoor.transform.Translate(0, 0, 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
     LeftDoor.transform.Translate(leftDoorReturnLoc, 0, 0);
     RightDoor.transform.Translate(rightDoorReturnLoc, 0, 0);
    }

}

