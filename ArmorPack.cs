using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPack : MonoBehaviour
{
    public PlayerHealth playerHealth;
    bool activated = false;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.Heal();


            Destroy(gameObject);
        }
    }
   
}
