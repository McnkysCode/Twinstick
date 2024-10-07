using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxArmor = 100;

    [SerializeField] private int currentHealth;
    [SerializeField] private int currentArmor;

    [SerializeField] private Image healthBarImage;
    [SerializeField] private Text healthText;
    [SerializeField] private int damage = 5;

    public healthbar HealthBar;
  
    public ArmorBar armorBar;

    public Transform respawnpoint;

    bool cheatHealth = false;
    private void Start()
    {

        currentHealth = maxHealth;
        currentArmor = maxArmor;

        HealthBar.SetMaxHealth(maxHealth);
        armorBar.SetMaxHealth(maxArmor);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            if (cheatHealth == false)
            {
                cheatHealth = true;
            }
            else if (cheatHealth == true)
            {
                cheatHealth = false;
            }
        }

    }

    public void TakeDamage()
    {
        if (cheatHealth)
        {
            return;
        }
        
        if (currentArmor <= 0)
        {
            currentHealth -= damage;
            HealthBar.SetHealth(currentHealth);
        }
        else if (currentArmor > 0)
        {
            currentArmor -= damage;
            armorBar.SetHealth(currentArmor);
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (currentHealth == 0)
        {
            RespawnPlayer();
        }

    }

    public void Heal()
    {

        if (currentHealth >= maxHealth)
        {
            currentArmor = maxArmor;
             armorBar.SetHealth(currentArmor);
        }
        if (currentHealth < maxHealth)
        {
            currentHealth = maxHealth;
            HealthBar.SetHealth(currentHealth);
        }

    }


    public void RespawnPlayer()
    {
        transform.position = respawnpoint.position;

        currentHealth = maxHealth;
        currentArmor = maxArmor;
        armorBar.SetMaxHealth(maxArmor);
        HealthBar.SetHealth(currentHealth);
    }

}
