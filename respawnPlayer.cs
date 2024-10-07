using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPlayer : MonoBehaviour
{
    public Transform respawnpoint;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()    
    {
        transform.position = respawnpoint.position;
    }
}
