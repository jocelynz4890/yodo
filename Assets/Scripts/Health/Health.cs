using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Health : MonoBehaviour {
    public int maxHealth = 3;
    public int currentHealth = 3;

    private TextMeshProUGUI text;
    private TextMeshProUGUI healthText;
    private Slider healthBar;

    void Start()
    {
        text = transform.Find("Canvas/Text").GetComponent<TextMeshProUGUI>();
        healthText = transform.Find("Canvas/Health/HealthText").GetComponent<TextMeshProUGUI>();
        healthBar = transform.Find("Canvas/Health/HealthBar").GetComponent<Slider>();

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
            currentHealth = Mathf.Max(0, currentHealth - damageAmount);
        }
        
        if (currentHealth == 0)
        {
            // Disable this object if it's a player. Otherwise, destroy it.
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<PlayerController>().enabled = false;
                text.text = "Wait for revive.";
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (currentHealth == 0)
        {
            // Revived so give back player controls
            gameObject.GetComponent<PlayerController>().enabled = true;
            text.text = "";
        }

        if (currentHealth < maxHealth)
        {
            // Add health to the player
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
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
