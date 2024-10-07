using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class PickUps : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;
    
    [SerializeField] public OpenDoors opendoor;
    [SerializeField] private PlayerHealth ph;
    [SerializeField] private healthbar HealthBar;
    [SerializeField] private ArmorBar armorBar;
    [SerializeField] private GunSystem gunSystem;

    [Header("pick up stats")]
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private KeyCode pickupKey = KeyCode.E;
    [SerializeField] private float PickupDistance = 1f;
    [SerializeField] private float moveSpeed = 5f;
    
    private Vector3 pickupPosition;
    private float elapsedTime;

    private bool test;

    [Header("keycard")]
    public bool redKeyPickedUp;
    public bool greenKeyPickedUp;
    public bool blueKeyPickedUp;

    void Update()
    {

        var  test = FindObjectOfType<clonePickUp>();

        if (!test)
            return;


        player = test.gameObject;

        target = player.transform;
        //Debug.Log(blueKeyPickedUp);
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= maxDistance && Input.GetKeyDown(pickupKey))
        {
            Pickup();
        }

       
        
        float distancePickup = Vector3.Distance(transform.position, target.position);
        if (distance <= maxDistance && CompareTag("PickUp"))
           {
               Vector3 direction = (target.position - transform.position).normalized;
               transform.position += direction * moveSpeed * Time.deltaTime;

           if (distance <= PickupDistance)
           {
               Pickup();
           }
        }
        
    }

    void Pickup()
    {
        if (gameObject.name == "")
        {
          blueKeyPickedUp = true;
            Debug.Log(blueKeyPickedUp);
         pickupPosition = transform.position;
            gameObject.SetActive(false);
        }
        if (gameObject.name == "P_key (1)")
        {
            redKeyPickedUp = true;
            pickupPosition = transform.position;
            gameObject.SetActive(false);
        }
        if (gameObject.name == "P_key")
        {
            greenKeyPickedUp = true;
            pickupPosition = transform.position;
            gameObject.SetActive(false);
        }
        if (gameObject.name == "Armour")
        {
            pickupPosition = transform.position;
            gameObject.SetActive(false);
            ph.Heal();
        }
        if (gameObject.name == "medkit1")
        {
            pickupPosition = transform.position;
            gameObject.SetActive(false);
            ph.Heal();
        }
        if (gameObject.name == "Ammobox")
        {
            pickupPosition = transform.position;
            gameObject.SetActive(false);
            gunSystem.Reload();
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PickupDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}