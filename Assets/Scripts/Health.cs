using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Health : MonoBehaviour {
    public int maxHealth = 3;
    public int currentHealth = 3;
    public Slider healthBar;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI healthText;

    void Start()
    {
        if (healthBar != null)
        {
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void Damage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            // Subtract damage from health
            currentHealth -= damageAmount;
        }
        
        if (currentHealth <= 0)
        {
            // Disable this object if it's a player. Otherwise, destroy it.
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<PlayerController>().enabled = false;
                deathText.gameObject.SetActive(true);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (healthBar != null)
        {
            // Update the health bar value based on the player's health
            healthBar.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }
}
