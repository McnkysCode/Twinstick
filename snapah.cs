using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapah : MonoBehaviour
{
    [SerializeField] private Transform player;  

    [SerializeField] private float shootCooldown = 2f;  
    [SerializeField] private int damage = 10;  

    [SerializeField] private PlayerHealth health;

    [SerializeField] private GameObject armorPackPrefab;
    [SerializeField] private GameObject medkitPrefab;

    [SerializeField] private int maxHealth;
    private int currentHealth;

    private float currentCooldown = 0f;  

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        currentCooldown -= Time.deltaTime;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (currentCooldown <= 0f && LineOfSightClear(direction, distance))
        {
            Shoot();
            currentCooldown = shootCooldown;  
        }
    }

    private bool LineOfSightClear(Vector3 direction, float distance)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return false;  
            }
            else if (hit.collider.CompareTag("Player"))
            {
                health.TakeDamage();
                return false;  
            }
        }

        return true;  
    }

    public void TakeDamage(int damage)
    {
        damage = 1;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        int dropType = Random.Range(0, 3);  //0: niks, 1: Armor Pack, 2: Medkit

        switch (dropType)
        {
            case 1:
                Instantiate(armorPackPrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(medkitPrefab, transform.position, Quaternion.identity);
                break;
            default:
                //niks
                break;
        }

        Destroy(gameObject);
    }

    private void Shoot()
    {
        Debug.Log("Enemy shoots!");
    }
}