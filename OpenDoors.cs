using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
   public Transform LeftDoor;
   public Transform RightDoor;
   [SerializeField]public float rightDoorLoc;
   [SerializeField]public float rightDoorReturnLoc;
   [SerializeField]public float leftDoorLoc;
   [SerializeField]public float leftDoorReturnLoc;

    public PickUps pickup;
    private bool blueKeyCard;
    private bool redKeyCard;
    private bool greenKeyCard;


    private void Start()
    {
       
    }

    private void Update()
    {
        redKeyCard = pickup.redKeyPickedUp;
        blueKeyCard = pickup.blueKeyPickedUp;
        greenKeyCard = pickup.greenKeyPickedUp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && redKeyCard==true || greenKeyCard==true || blueKeyCard == true)
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
       if(!other.CompareTag("Player") && redKeyCard == false || greenKeyCard == false || blueKeyCard == false)
       {
            LeftDoor.transform.Translate(0, 0, 0);
            RightDoor.transform.Translate(0, 0, 0);
       }
        else
        {
            LeftDoor.transform.Translate(leftDoorReturnLoc, 0, 0);
            RightDoor.transform.Translate(rightDoorReturnLoc, 0, 0);

        }
            

    }

}
