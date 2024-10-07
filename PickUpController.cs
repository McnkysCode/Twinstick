using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        if (!equipped)
        {
            //gunscript.eneabled= false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
          //gunscript.eneabled= true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }
    private void Update()
    {
        //check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.R) && !slotFull) PickUp();

        //drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.F)) Drop();
    }
    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //make weaoon a child of the camera and move it to default pos
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //make rigidbody kinematic and boxcollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //enable script
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        //gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //addforce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        //add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);


        //make rigidbody not kinematic and boxcollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //disable script
    }


}
