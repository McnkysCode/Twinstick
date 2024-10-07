using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
    public float despawnTime = 2f;
    

    private PlayerHealth health;
    private void Start()
    {
        Invoke("DespawnBullet", despawnTime);
    }

    private void Update()
    {

    }

    private void DespawnBullet()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}
